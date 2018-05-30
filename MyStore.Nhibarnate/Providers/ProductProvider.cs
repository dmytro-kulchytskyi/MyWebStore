
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

        public Product GetByExternalProductId(string externalProductId)
        {
            return providerHelper.Invoke(s => s.QueryOver<Product>()
                .Where(it => it.ExternalProductId == externalProductId).SingleOrDefault());
        }

        //public IList<Product> GetSegmentOrderedByBy(string fieldName, bool oredrByDesc, int count, string lastLoadedId)
        //{
        //    if (count < 1)
        //        throw new ArgumentException("Count must be positive number");

        //    return providerHelper.Invoke(s =>
        //    {
        //        var firstResultIdCriteria = DetachedCriteria.For<Product>()
        //        .AddOrder(oredrByDesc ?
        //            NHibernate.Criterion.Order.Desc(Projections.Property(fieldName)) :
        //            NHibernate.Criterion.Order.Desc(Projections.Property(fieldName)))
                    
        //        .SetMaxResults(1)
        //        .SetProjection(Projections.Property("Id"));
                
        //        return new List<Product>();
        //    });
        //}

        public IList<Product> GetTopBySellingCount(int count, bool includeBanned = false)
        {
            if (count < 1)
                throw new ArgumentException("Count must be positive number");

            return providerHelper.Invoke(s =>
            {
                var query = s.QueryOver<Product>();

                if (!includeBanned)
                    query = query.Where(it => !it.Banned);

                return query.OrderBy(it => it.SellsCount).Desc.Take(count).List();
            });
        }
    }
}
