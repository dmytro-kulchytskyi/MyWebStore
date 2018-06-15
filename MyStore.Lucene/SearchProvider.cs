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

        protected FSDirectory Directory
        {
            get
            {
                if (WorkDirectory == null)
                    throw new InvalidOperationException("Work direcotory required");

                if (!WorkDirectory.Exists)
                    WorkDirectory.Create();

                return FSDirectory.Open(WorkDirectory);
            }
        }

        protected IndexWriter GetIndexWriter()
        {
            return new IndexWriter(Directory, GetAnalyzer(), IndexWriter.MaxFieldLength.UNLIMITED);
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

        public ListSegment<SearchResult> Search(SearchOptions searchOptions)
        {
            if (searchOptions == null)
                throw new ArgumentNullException(nameof(searchOptions));

            if (searchOptions.ResultFields == null || searchOptions.ResultFields.Count() == 0)
                throw new ArgumentException($"{nameof(searchOptions.ResultFields)} required");

            if (searchOptions.PageSize < 0)
                throw new ArgumentException($"{searchOptions.PageSize} can't be negative");

            var unstoredFields = searchOptions.ResultFields.Where(it => !StoredFields.Contains(it));

            if (unstoredFields.Count() > 0)
                throw new InvalidOperationException("Some of the requested fields are unstored: " + string.Join(", ", unstoredFields));

            ListSegment<SearchResult> results = new ListSegment<SearchResult>();

            if (string.IsNullOrEmpty(searchOptions.Query))
                return results;
            
            var searchQuery = string.Join(" AND ", QueryParser.Escape(searchOptions.Query.Trim())
                .Split().Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim() + "*"));

            if (string.IsNullOrWhiteSpace(searchQuery))
                return results;

            using (var searcher = new IndexSearcher(Directory, true))
            {
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
                
                Sort sort;
                if (string.IsNullOrEmpty(searchOptions.SortField))
                {
                    sort = Sort.RELEVANCE;
                }
                else
                {
                    sort = new Sort(new SortField(searchOptions.SortField, SortField.STRING, searchOptions.InverseSort));
                }

                var pSize = searchOptions.PageSize;
                var pNumber = searchOptions.PageNumber;

                var hitsLimit = (pNumber + 1) * pSize;

                var docs = searcher.Search(parser.Parse(searchQuery), null, hitsLimit, sort);

                var totalHits = docs.TotalHits;

                var hits = docs.ScoreDocs;

                var resultFieldsArray = searchOptions.ResultFields.ToArray();

                var items = new List<SearchResult>(searchOptions.PageSize);

                var count = 0;
                for (var i = pNumber * pSize; count < pSize; i++, count++)
                {
                    if (i == totalHits)
                        break;

                    var doc = searcher.Doc(hits[i].Doc);
                    var result = new Dictionary<string, string>(resultFieldsArray.Length);

                    foreach (var key in resultFieldsArray)
                        result.Add(key, doc.Get(key));

                    items.Add(new SearchResult(result, resultFieldsArray));
                }

                results.Items = items;
                results.TotalCount = totalHits;

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
            Clear(new string[] { id });
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
