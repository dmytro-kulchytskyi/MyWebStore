using MyStore.Business.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Search
{
    public interface ISearchProviderFactory<T>
        where T : IEntity
    {
        ISearchProvider<T> GetProvider(DirectoryInfo directory);
        ISearchProvider<T> GetProvider(string directoryPath);
    }
}
