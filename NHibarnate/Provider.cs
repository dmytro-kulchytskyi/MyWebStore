using DimasikStore.Business.Entities;
using DimasikStore.Business.Providers;
using DimasikStore.Nhibernate.Wrappers.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimasikStore.Nhibarnate
{
    public class Provider<T> : IProvider<T>
        where T : IEntity
    {
        private readonly TransactionWrapperFactory transactionWrapperFactory;

        public Provider(TransactionWrapperFactory transactionWrapperFactory)
        {
            this.transactionWrapperFactory = transactionWrapperFactory;
        }
        protected UnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork(transactionWrapperFactory);
        }

        public Task Delete(T instance)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(T instance)
        {
            throw new NotImplementedException();
        }

        public Task<T> Save(T instance)
        {
            throw new NotImplementedException();
        }
    }
}
