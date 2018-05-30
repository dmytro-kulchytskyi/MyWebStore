using System;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStore.Business.Entities;
using Version = Lucene.Net.Util.Version;
using MyStore.Business.Search;
using MyStore.Business;
using MyStore.Lucene;

namespace MyStore.SearchProvider
{
    public abstract class SearchProvider<T> : ISearchProvider<T>
        where T : class, IEntity
    {
        protected readonly Version luceneVersion = Version.LUCENE_30;

        protected readonly int searchResultsDefaultLimit = Configuration.SearchResultsDefaultLimit;

        public SearchProvider(DirectoryInfo directory)
        {
            WorkDirectory = directory;
        }

        protected abstract string[] DefaultSearchFields { get; }

        protected abstract string[] StoredFields { get; }

        protected abstract Document MapInstanceToDocument(T instance);

        public DirectoryInfo WorkDirectory { get; private set; }

        protected Analyzer GetAnalyzer()
        {
            return new StandardAnalyzer(luceneVersion);
        }

        protected FSDirectory FSDirectory
        {
            get
            {
                if (WorkDirectory == null)
                    throw new InvalidOperationException("Work direcotory required");

                if (!WorkDirectory.Exists)
                    WorkDirectory.Create();

                var directory = FSDirectory.Open(WorkDirectory);

                if (IndexWriter.IsLocked(directory))
                    IndexWriter.Unlock(directory);

                var lockFilePath = Path.Combine(WorkDirectory.FullName, "write.lock");

                if (File.Exists(lockFilePath))
                    File.Delete(lockFilePath);

                return directory;
            }
        }

        protected IndexWriter GetIndexWriter()
        {
            return new IndexWriter(FSDirectory, GetAnalyzer(), IndexWriter.MaxFieldLength.UNLIMITED);
        }

        protected Query ParseQuery(string searchQuery, QueryParser parser)
        {
            try
            {
                return parser.Parse(searchQuery.Trim());
            }
            catch (ParseException)
            {
                return parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
        }

        public void AddOrUpdate(IEnumerable<T> instances)
        {
            using (var writer = GetIndexWriter())
            {
                foreach (var instance in instances)
                {
                    var searchQuery = new TermQuery(new Term("Id", instance.Id));
                    writer.DeleteDocuments(searchQuery);
                    writer.AddDocument(MapInstanceToDocument(instance));
                }

                writer.Analyzer.Dispose();
            }
        }

        public void AddOrUpdate(T instance)
        {
            AddOrUpdate(new List<T> { instance });
        }


        public IEnumerable<Dictionary<string, string>> Search(SearchOptions searchOptions)
        {
            if (searchOptions == null)
                throw new ArgumentNullException(nameof(searchOptions));

            if (searchOptions.ResultFields == null || searchOptions.ResultFields.Count() == 0)
                throw new ArgumentException($"{nameof(searchOptions.ResultFields)} required");

            if (searchOptions.MaxResults < 0)
                throw new ArgumentException($"{searchOptions.MaxResults} can't be negative");

            var unstoredFields = searchOptions.ResultFields.Where(it => !StoredFields.Contains(it));

            if (unstoredFields.Count() > 0)
                throw new InvalidOperationException("Some of the requested fields are unstored: " + string.Join(", ", unstoredFields));

            IEnumerable<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            if (string.IsNullOrEmpty(searchOptions.Query))
                return results;

            var searchQuery = string.Join(" ", searchOptions.Query.Trim().Replace("-", " ").Split(' ')
             .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim() + "*"));

            if (string.IsNullOrWhiteSpace(searchQuery))
                return results;

            using (var searcher = new IndexSearcher(FSDirectory, true))
            {
                var hitsLimit = searchOptions.MaxResults != 0 ? searchResultsDefaultLimit : searchOptions.MaxResults;
                var analyzer = GetAnalyzer();

                QueryParser parser;
                if (searchOptions.SearchFields == null || searchOptions.SearchFields.Count() == 0)
                {
                    parser = new MultiFieldQueryParser(luceneVersion, DefaultSearchFields, analyzer);
                }
                else if (searchOptions.SearchFields.Count() == 1)
                {
                    parser = new QueryParser(luceneVersion, searchOptions.SearchFields.First(), analyzer);
                }
                else
                {
                    parser = parser = new MultiFieldQueryParser(luceneVersion, searchOptions.SearchFields.ToArray(), analyzer);
                }

                var query = ParseQuery(searchQuery, parser);

                Sort sort;
                if (string.IsNullOrEmpty(searchOptions.SortField))
                {
                    sort = Sort.RELEVANCE;
                }
                else
                {
                    sort = new Sort(new SortField(searchOptions.SortField, SortField.STRING, searchOptions.InverseOrder));
                }

                var hits = searcher.Search(query, null, hitsLimit, sort).ScoreDocs;

                results = hits.Select(hit =>
                {
                    var doc = searcher.Doc(hit.Doc);
                    var result = new Dictionary<string, string>(searchOptions.ResultFields.Count());

                    foreach (var key in searchOptions.ResultFields)
                    {
                        result.Add(key, doc.Get(key));
                    }

                    return result;
                }).ToList();

                analyzer.Dispose();
            }

            return results;
        }

        public void Clear(IEnumerable<string> ids)
        {
            using (var writer = GetIndexWriter())
            {
                foreach (var id in ids)
                {
                    var searchQuery = new TermQuery(new Term("Id", id));
                    writer.DeleteDocuments(searchQuery);
                }

                writer.Analyzer.Dispose();
            }
        }

        public void Clear(string id)
        {
            Clear(new List<string> { id });
        }

        public void Optimize()
        {
            using (var writer = GetIndexWriter())
            {
                writer.Optimize();
                writer.Analyzer.Dispose();
            }
        }
    }
}
