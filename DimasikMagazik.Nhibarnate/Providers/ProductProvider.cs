using DimasikMagazik.Business.Entities;
using DimasikMagazik.Business.Providers;
using DimasikMagazik.Nhibernate.Wrappers.Factories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DimasikMagazik.Nhibarnate.Providers
{
    public class ProductProvider : Provider<Product>, IProductProvider
    {
        public ProductProvider(SessionWrapperFactory sessionWrapperFactory, TransactionWrapperFactory transactionWrapperFactory)
            : base(sessionWrapperFactory, transactionWrapperFactory)
        {
        }

        public Task<IList<Product>> GetSegmentOrderedByByDate(int count, DateTime startDate)
        {
            if (count < 1)
                throw new ArgumentException("Count must be positive number");

            return providerHelper.Invoke(s => s.QueryOver<Product>()
                    .OrderBy(it => it.Added).Desc
                    .Where(it => it.Added < startDate)
                    .Take(count).ListAsync());
        }

        public Task<IList<Product>> GetTopBySellingCount(int count, bool includeBanned = false)
        {
            if (count < 1)
                throw new ArgumentException("Count must be positive number");

            return providerHelper.Invoke(s =>
            {
                var query = s.QueryOver<Product>();

                if (!includeBanned)
                    query = query.Where(it => !it.Banned);

                return query.OrderBy(it => it.SellsCount).Desc.Take(count).ListAsync();
            });
        }
    }
}
