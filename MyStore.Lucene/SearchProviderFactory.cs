using MyStore.Business.Entities;
using MyStore.Business.Search;
using MyStore.SearchProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Lucene
{
    public class ProductSearchProviderFactory: ISearchProviderFactory<Product>
    {
        public ISearchProvider<Product> GetProvider(DirectoryInfo directory)
        {
            return new ProductSearchProvider(directory);
        }

        public ISearchProvider<Product> GetProvider(string directoryPath)
        {
            return new ProductSearchProvider(new DirectoryInfo(directoryPath));
        }
    }
}
