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
        Dictionary<string, string> SearchOne(string searchQuery, IEnumerable<string> resultFields, IEnumerable<string> searchFields = null);
        IEnumerable<Dictionary<string, string>> Search(string searchQuery, IEnumerable<string> resultFields, int maxResults = 0, IEnumerable<string> searchFields = null);
        void AddOrUpdate(T entity);
        void AddOrUpdate(IEnumerable<T> entities);
        void Optimize();
        void Clear(string id);
        void Clear(IEnumerable<string> ids);
    }
}
