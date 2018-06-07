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
        private static readonly object reindexndexLock = new object();

        private static IndexStatus indexStatus;

        private static double indexProgress;

        private static string indexError;

        private readonly ProductManager productManager;

        private readonly ProductSearchManager productSearchManager;

        private readonly ISearchProviderFactory<Product> searchProviderFactory;

        public IndexStatus IndexStatus => indexStatus;

        public double IndexProgress => indexProgress;

        public string IndexError => indexError;

        public ProductISearchIndexManager(ProductManager productManager,
                                          ProductSearchManager productSearchManager,
                                          ISearchProviderFactory<Product> searchProviderFactory)
        {
            this.searchProviderFactory = searchProviderFactory;
            this.productManager = productManager;
            this.productSearchManager = productSearchManager;
        }

        public void CreateSearchIndex()
        {
            lock (reindexndexLock)
            {
                if (indexStatus == IndexStatus.InProgress)
                    throw new InvalidOperationException("Indexation task already in progress");

                indexStatus = IndexStatus.InProgress;
                indexProgress = 0;
            }

            Task.Run(() =>
            {
                try
                {
                    var tempDir = new DirectoryInfo(Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        AppConfiguration.SearchManagerFolderName,
                        Guid.NewGuid().ToString()));

                    var searchProvider = searchProviderFactory.GetProvider(tempDir);

                    var batchSize = 100;
                    var pageCount = (int)Math.Ceiling(productManager.GetCount() / (double)batchSize);
                    for (var page = 0; page < pageCount; page++)
                    {
                        var products = productManager.GetPageOrderedBy(ProductFields.Title, false, batchSize, page).Items;
                        searchProvider.AddOrUpdate(products);
                        indexProgress = (double)page / pageCount;
                    }

                    searchProvider.Optimize();

                    var path = productSearchManager.DirectoryPath;

                    if (Directory.Exists(path))
                        new DirectoryInfo(path).Delete(true);

                    tempDir.MoveTo(path);

                    indexStatus = IndexStatus.Success;
                }
                catch (Exception e)
                {
                    indexStatus = IndexStatus.Failed;
                    indexError = e.Message;
                }
            });
        }
    }
}
