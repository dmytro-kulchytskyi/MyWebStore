﻿using MyStore.Business;
using MyStore.Business.Entities;
using MyStore.Business.Providers;
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
    public class BusinessEntityProvider<T> : Provider<T>, IProvider<T>
        where T : class, IEntity
    {
        public BusinessEntityProvider(SessionWrapperFactory sessionWrapperFactory, TransactionWrapperFactory transactionWrapperFactory) 
            : base(sessionWrapperFactory, transactionWrapperFactory)
        {
        }

        public virtual int GetCount()
        {
            return providerHelper.Invoke(s => s.QueryOver<T>().RowCount());
        }

        public virtual IList<T> GetById(IEnumerable<string> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            if (ids.Count() == 0)
                return new List<T>();

            return providerHelper.Invoke(s => s.QueryOver<T>()
                .WhereRestrictionOn(it => it.Id).IsIn(new Collection<string>(ids.ToList())).List());
        }

        public virtual void Delete(T instance)
        {
            using (var uow = GetUnitOfWork())
            {
                uow.BeginTransaction();
                providerHelper.Invoke(s => s.Delete(instance));
                uow.Commit();
            }
        }

        public virtual void Delete(string id)
        {
            using (var uow = GetUnitOfWork())
            {
                uow.BeginTransaction();
                providerHelper.Invoke(s => s.Delete(s.Load<T>(id)));
                uow.Commit();
            }
        }

        public virtual T GetById(string id)
        {
            return providerHelper.Invoke(s => s.Get<T>(id));
        }

        public virtual void Update(T instance)
        {
            using (var uow = GetUnitOfWork())
            {
                uow.BeginTransaction();
                providerHelper.Invoke(s => s.Update(instance));
                uow.Commit();
            }
        }

        public virtual T Save(T instance)
        {
            if (string.IsNullOrEmpty(instance.Id))
                instance.Id = Guid.NewGuid().ToString();

            using (var uow = GetUnitOfWork())
            {
                uow.BeginTransaction();
                providerHelper.Invoke(s => s.Save(instance));
                uow.Commit();
            }

            return instance;
        }

        public virtual ListSegment<T> GetPageSortedBy(string fieldName, bool inverseSort, int pageSize, int pageNumber)
        {
            return providerHelper.Invoke(s =>
            {                var query = s.QueryOver<T>().OrderBy(Projections.Property(fieldName));
                var items = (inverseSort ? query.Desc : query.Asc).Skip(pageNumber * pageSize).Take(pageSize).List();
                var totalCount = s.QueryOver<T>().RowCount();

                return new ListSegment<T>
                {
                    Items = items,
                    TotalCount = totalCount
                };
            });
        }
    }
}
