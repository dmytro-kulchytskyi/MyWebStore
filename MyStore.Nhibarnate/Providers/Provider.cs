using MyStore.Business;
using MyStore.Business.Entities;
using MyStore.Business.Providers;
using MyStore.Nhibarnate;
using MyStore.Nhibernate.Wrappers.Factories;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate.Providers
{
    public class Provider<T>
        where T : class
    {
        protected readonly TransactionWrapperFactory transactionWrapperFactory;

        protected readonly SessionWrapperFactory sessionWrapperFactory;

        protected readonly ProviderHelper providerHelper;

        public Provider(SessionWrapperFactory sessionWrapperFactory, TransactionWrapperFactory transactionWrapperFactory)
        {
            this.transactionWrapperFactory = transactionWrapperFactory;

            providerHelper = new ProviderHelper(this.sessionWrapperFactory = sessionWrapperFactory);
        }

        protected UnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork(transactionWrapperFactory);
        }

       
    }
}
