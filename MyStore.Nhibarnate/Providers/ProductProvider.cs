
using MyStore.Business;
using MyStore.Business.Entities;
using MyStore.Business.Providers;
using MyStore.Nhibernate.Wrappers.Factories;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate.Providers
{
    public class ProductProvider : Provider<Product>, IProductProvider
    {
        public ProductProvider(SessionWrapperFactory sessionWrapperFactory, TransactionWrapperFactory transactionWrapperFactory)
            : base(sessionWrapperFactory, transactionWrapperFactory)
        {
        }


        public IList<Product> GetTop(string fieldName, int count, bool includeBanned = false)
        {
            if (count < 1)
                throw new ArgumentException("Count must be positive number");

            return providerHelper.Invoke(s =>
            {
                var query = s.QueryOver<Product>();

                if (!includeBanned)
                    query = query.Where(it => !it.Banned);

                return query.OrderBy(Projections.Property(fieldName)).Desc.Take(count).List();
            });
        }

    }
}
