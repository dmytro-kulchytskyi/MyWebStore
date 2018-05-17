using MyStore.Business.Entities;
using MyStore.Business.Providers;
using MyStore.Business.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Managers
{
    public class ProductManager : Manager<Product, IProductProvider>
    {
        private readonly ISearchProvider<Product> searchProvider;

        private static readonly object indexLock = new object();

        private static bool indexInProgress;

        private static double indexProgress;

        public ProductManager(IProductProvider provider, ISearchProvider<Product> searchProvider) : base(provider)
        {
            this.searchProvider = searchProvider;
        }

        public Product GetByExternalProductId(string externalProductId)
        {
            return provider.GetByExternalProductId(externalProductId);
        }

        public IList<Product> GetTopBySellingCount(int count)
        {
            return provider.GetTopBySellingCount(count);
        }

        public IList<Product> GetSegmentOrderedByByDate(int count, DateTime startDate)
        {
            return provider.GetSegmentOrderedByByDate(count, startDate);
        }

        public IList<Product> Search(string query, int maxResults = 0, string fieldName = null)
        {
            var ids = searchProvider.Search(query, maxResults, fieldName);
            return provider.GetById(ids);
        }

        public void CreateSearchIndex()
        {

            lock (indexLock)
            {
                if (indexInProgress)
                    throw new InvalidOperationException("Already indexing");

                indexInProgress = true;
            }

            Task.Run(() =>
            {
                var indexationSegmentSize = 100;
                var pageCount = (int)Math.Ceiling(provider.GetCount() / (double)indexationSegmentSize);
                for (var page = 0; page < pageCount; page++)
                {
                    var products = provider.GetPage(indexationSegmentSize, page);
                    searchProvider.AddOrUpdate(products);
                    indexProgress = pageCount / (double)page;
                }

                lock (indexLock) indexInProgress = false;
            });
        }

        public bool GetIndexInProgress() => indexInProgress;

        public double GetIndexProgress() => indexProgress;
    }
}
