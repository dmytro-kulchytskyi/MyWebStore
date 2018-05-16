using MyStore.Business.Entities;
using MyStore.Business.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Managers
{
    public class ProductManager : Manager<Product, IProductProvider>
    {
        public ProductManager(IProductProvider provider) : base(provider)
        {
        }

        public Task<IList<Product>> GetTopBySellingCount(int count)
        {
            return provider.GetTopBySellingCount(count);
        }

        public Task<IList<Product>> GetSegmentOrderedByByDate(int count, DateTime startDate)
        {
            return provider.GetSegmentOrderedByByDate(count, startDate);
        }
    }
}
