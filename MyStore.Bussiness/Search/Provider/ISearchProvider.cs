using MyStore.Business.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Search
{
    public interface ISearchProvider<T>
       where T : IEntity
    {
        DirectoryInfo WorkDirectory { get; }
        ListSegment<SearchResult> Search(SearchOptions searchOption);
        void AddOrUpdate(T entity);
        void AddOrUpdate(IEnumerable<T> entities);
        void Optimize();
        void Clear(string id);
        void Clear(IEnumerable<string> ids);
    }
}
