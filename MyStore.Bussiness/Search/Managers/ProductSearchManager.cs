using Microsoft.VisualBasic;
using MyStore.Business.Entities;
using MyStore.Business.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Search.Managers
{
    public class ProductSearchManager
    {
        private readonly ProductManager productManager;

        private readonly ISearchProviderFactory<Product> searchProviderFactory;

        private readonly ISearchProvider<Product> searchProvider;

        public static readonly string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConfiguration.SearchManagerFolderName, nameof(Product));

        public string DirectoryPath => directoryPath;

        public ProductSearchManager(ProductManager productManager, ISearchProviderFactory<Product> searchProviderFactory)
        {
            this.productManager = productManager;
            this.searchProviderFactory = searchProviderFactory;

            searchProvider = searchProviderFactory.GetProvider(directoryPath);
        }

        public ListSegment<SearchResult> Search(SearchOptions searchOptions)
        {
            return searchProvider.Search(searchOptions);
        }
    }
}
