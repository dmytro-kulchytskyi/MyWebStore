﻿using MyStore.Business.Entities;
using MyStore.Business.Providers;
using MyStore.Nhibarnate;
using MyStore.Nhibernate.Wrappers.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate.Providers
{
    public class Provider<T> : IProvider<T>
        where T : class, IEntity
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

        public int GetCount()
        {
            return providerHelper.Invoke(s => s.QueryOver<T>().RowCount());
        }

        public IList<T> GetById(IEnumerable<string> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            if (ids.Count() == 0)
                return new List<T>();

            return providerHelper.Invoke(s => s.QueryOver<T>().WhereRestrictionOn(it => it.Id).IsIn(new Collection<string>(ids.ToList())).List());
        }

        public void Delete(T instance)
        {
            using (var uow = GetUnitOfWork())
            {
                uow.BeginTransaction();
                providerHelper.Invoke(s => s.Delete(instance));
                uow.Commit();
            }
        }

        public void Delete(string id)
        {
            using (var uow = GetUnitOfWork())
            {
                uow.BeginTransaction();
                providerHelper.Invoke(s => s.Delete(s.Load<T>(id)));
                uow.Commit();
            }
        }

        public T GetById(string id)
        {
            return providerHelper.Invoke(s => s.Get<T>(id));
        }

        public void Update(T instance)
        {
            using (var uow = GetUnitOfWork())
            {
                uow.BeginTransaction();
                providerHelper.Invoke(s => s.Update(instance));
                uow.Commit();
            }
        }

        public T Save(T instance)
        {
            instance.Id = Guid.NewGuid().ToString();

            using (var uow = GetUnitOfWork())
            {
                uow.BeginTransaction();
                providerHelper.Invoke(s => s.Save(instance));
                uow.Commit();
            }

            return instance;
        }

        public IList<T> GetPage(int count, int pageNumber)
        {
            return providerHelper.Invoke(s => s.QueryOver<T>().Skip(pageNumber * count).Take(count).List());
        }
    }
}
