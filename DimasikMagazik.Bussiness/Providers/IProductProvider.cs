using DimasikMagazik.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimasikMagazik.Business.Providers
{
    public interface IProductProvider : IProvider<Product>
    {
        Task<IList<Product>> GetTopBySellingCount(int count, bool includeBanned = false);
        Task<IList<Product>> GetSegmentOrderedByByDate(int maxResults, DateTime startDate);
    }
}
