using MyStore.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Providers
{
    public interface IProductProvider : IProvider<Product>
    {
        IList<Product> GetTopBySellingCount(int count, bool includeBanned = false);
        Product GetByExternalProductId(string externalProductId);
    }
}
