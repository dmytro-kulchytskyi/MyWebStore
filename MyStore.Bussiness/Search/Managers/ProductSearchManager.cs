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

        private readonly ISearchIndexInfoProvider searchIndexInfoProvider;

        private readonly ISearchProviderFactory<Product> searchProviderFactory;

        private readonly Lazy<ISearchProvider<Product>> searchProvider;

        private ISearchProvider<Product> SearchProvider => searchProvider.Value;

        public static readonly string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConfiguration.SearchManagerFolderName, nameof(Product));

        public string DirectoryPath => directoryPath;

        public ProductSearchManager(ProductManager productManager, ISearchIndexInfoProvider searchIndexInfoProvider, ISearchProviderFactory<Product> searchProviderFactory)
        {
            this.productManager = productManager;
            this.searchIndexInfoProvider = searchIndexInfoProvider;
            this.searchProviderFactory = searchProviderFactory;

            searchProvider = new Lazy<ISearchProvider<Product>>(() =>
            {
                var searchIndexInfo = searchIndexInfoProvider.GetCurrentSearchIndexInfo();

                if (searchIndexInfo == null)
                    throw new InvalidOperationException("Cant get current search index info");

                return searchProviderFactory.GetProvider(searchIndexInfo.IndexFilesLocation);
            });
        }

        public ListSegment<SearchResult> Search(SearchOptions searchOptions)
        {
            return SearchProvider.Search(searchOptions);
        }
    }
}
