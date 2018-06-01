using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Search.Managers
{
    public partial class ProductSearchManager
    {
        private static readonly object reindexndexLock = new object();

        private static IndexStatus indexStatus;

        private static double indexProgress;

        private static string indexError;

        public IndexStatus IndexStatus => indexStatus;

        public double IndexProgress => indexProgress;

        public string IndexError => indexError;

        public void CreateSearchIndex()
        {
            lock (reindexndexLock)
            {
                if (indexStatus == IndexStatus.InProgress)
                    throw new InvalidOperationException("Already indexing");

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
                        var products = productManager.GetPageOrderedBy(ProductFields.Title, true, batchSize, page).Items;
                        searchProvider.AddOrUpdate(products);
                        indexProgress = (double)page / pageCount;
                    }

                    searchProvider.Optimize();

                    if (Directory.Exists(directoryPath))
                        new DirectoryInfo(directoryPath).Delete(true);

                    tempDir.MoveTo(directoryPath);
                }
                catch (Exception e)
                {
                    lock (reindexndexLock)
                    {
                        indexStatus = IndexStatus.Failed;
                        indexError = e.Message;
                    }

                    return;
                }

                lock (reindexndexLock)
                    indexStatus = IndexStatus.Success;

            });
        }
    }
}
