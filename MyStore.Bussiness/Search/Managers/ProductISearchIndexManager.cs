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
    public class ProductISearchIndexManager
    {
        private readonly ProductManager productManager;

        private readonly ISearchProviderFactory<Product> searchProviderFactory;

        private readonly ISearchIndexInfoProvider searchIndexInfoProvider;

        private static double indexProgressPercentage;

        public ProductISearchIndexManager(ProductManager productManager,
                                          ProductSearchManager productSearchManager,
                                          ISearchIndexInfoProvider searchIndexInfoProvider,
                                          ISearchProviderFactory<Product> searchProviderFactory)
        {
            this.searchIndexInfoProvider = searchIndexInfoProvider;
            this.searchProviderFactory = searchProviderFactory;
            this.productManager = productManager;
            this.ProductSearchManager = productSearchManager;
        }

        public ProductSearchManager ProductSearchManager { get; private set; }

        public double GetCurrentIndexProgressPercentage()
        {
            return indexProgressPercentage;
        }

        public SearchIndexInfo GetCurrentSearchIndexInfo()
        {
            return searchIndexInfoProvider.GetCurrentSearchIndexInfo();
        }

        public void CreateSearchIndex()
        {
            var currentSearchIndexInfo = GetCurrentSearchIndexInfo();

            searchIndexInfoProvider.EnterLock();

            searchIndexInfoProvider.SaveCurrentSearchIndexInfo(new SearchIndexInfo()
            {
                Date = DateTime.Now,
                IndexInProgress = true,
            });

            Task.Run(() =>
            {
                var indexDirectory = new DirectoryInfo(Path.Combine(
                       AppDomain.CurrentDomain.BaseDirectory,
                       AppConfiguration.SearchManagerFolderName,
                       Guid.NewGuid().ToString()));

                try
                {
                    var searchProvider = searchProviderFactory.GetProvider(indexDirectory);

                    var batchSize = 100;
                    var pageCount = (int)Math.Ceiling(productManager.GetCount() / (double)batchSize);
                    for (var page = 0; page < pageCount; page++)
                    {
                        var products = productManager.GetPageSortedBy(ProductFields.Title, false, batchSize, page).Items;
                        searchProvider.AddOrUpdate(products);
                        indexProgressPercentage = 100 * ((page + 1) / (double)pageCount);
                    }

                    searchProvider.Optimize();

                    searchIndexInfoProvider.SaveCurrentSearchIndexInfo(new SearchIndexInfo
                    {
                        IndexFilesLocation = indexDirectory.FullName,
                        Date = DateTime.Now,
                        IndexSuccess = true,
                        IndexFinished = true
                    });

                    if (currentSearchIndexInfo != null)
                        if (Directory.Exists(currentSearchIndexInfo.IndexFilesLocation))
                            Directory.Delete(currentSearchIndexInfo.IndexFilesLocation, true);
                }
                catch (Exception e)
                {
                    searchIndexInfoProvider.SaveCurrentSearchIndexInfo(new SearchIndexInfo
                    {
                        Date = DateTime.Now,
                        IndexSuccess = false,
                        IndexFinished = true,
                        IndexErrorMessage = e.Message,
                        IndexErrorStackTrace = e.StackTrace
                    });

                    if (Directory.Exists(indexDirectory.FullName))
                        Directory.Delete(indexDirectory.FullName, true);

                    throw;
                }
                finally
                {
                    searchIndexInfoProvider.ExitLock();
                }
            });
        }
    }
}
