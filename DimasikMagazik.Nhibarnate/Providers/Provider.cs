using DimasikMagazik.Business.Entities;
using DimasikMagazik.Business.Providers;
using DimasikMagazik.Nhibarnate;
using DimasikMagazik.Nhibernate.Wrappers.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimasikMagazik.Nhibarnate.Providers
{
    public class Provider<T> : IProvider<T>
        where T : IEntity
    {
        private readonly TransactionWrapperFactory transactionWrapperFactory;

        protected readonly ProviderHelper providerHelper;

        public Provider(SessionWrapperFactory sessionWrapperFactory, TransactionWrapperFactory transactionWrapperFactory)
        {
            this.transactionWrapperFactory = transactionWrapperFactory;

            providerHelper = new ProviderHelper(sessionWrapperFactory);
        }

        protected UnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork(transactionWrapperFactory);
        }

        public async Task Delete(T instance)
        {
            using (var uow = GetUnitOfWork())
            {
                uow.BeginTransaction();
                await providerHelper.Invoke(s => s.DeleteAsync(instance));
                await uow.Commit();
            }
        }

        public async Task Delete(string id)
        {
            using (var uow = GetUnitOfWork())
            {
                uow.BeginTransaction();
                await providerHelper.Invoke(async s => s.DeleteAsync(await s.LoadAsync<T>(id)));
                await uow.Commit();
            }
        }

        public Task<T> GetById(string id)
        {
            return providerHelper.Invoke(s => s.GetAsync<T>(id));
        }

        public async Task Update(T instance)
        {
            using (var uow = GetUnitOfWork())
            {
                uow.BeginTransaction();
                await providerHelper.Invoke(s => s.UpdateAsync(instance));
                await uow.Commit();
            }
        }

        public async Task<T> Save(T instance)
        {
            instance.Id = Guid.NewGuid().ToString();

            using (var uow = GetUnitOfWork())
            {
                uow.BeginTransaction();
                await providerHelper.Invoke(s => s.SaveAsync(instance));
                await uow.Commit();
            }

            return instance;
        }
    }
}
