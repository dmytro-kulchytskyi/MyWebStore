using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyStore.Business;
using MyStore.Nhibernate.Wrappers.Factories;

namespace MyStore.Nhibernate.Wrappers
{
    public class TransactionWrapper : IDisposable
    {
        private const string CurrentTransactionContainerKey = "CurrentTransactionWrapper";

        private readonly IRequestDataStorage requestDataStorage;

        private readonly TransactionWrapper parent;

        private readonly SessionWrapper sessionWrapper;

        public TransactionWrapper(IRequestDataStorage requestDataStorage, SessionWrapperFactory sessionWrapperFactory)
        {
            this.requestDataStorage = requestDataStorage;

            sessionWrapper = sessionWrapperFactory.Create();

            parent = requestDataStorage.GetValue<TransactionWrapper>(CurrentTransactionContainerKey);
            requestDataStorage.SetValue(CurrentTransactionContainerKey, this);
        }

        public ITransaction Transaction { get; private set; }

        public bool IsActive => Transaction?.IsActive ?? false;

        public bool IsCommited => Transaction?.WasCommitted ?? false;

        public bool IsRolledBack => Transaction?.WasRolledBack ?? false;

        private bool IsBaseTransaction => !parent?.IsActive != true;

        public void Begin()
        {
            if (Transaction?.IsActive != true)
            {
                Transaction = IsBaseTransaction ? 
                    sessionWrapper.Session.BeginTransaction() : parent.Transaction;
            }
        }

        public void Rollback()
        {
            Transaction.Rollback();
        }

        public void Commit()
        {
            if (IsBaseTransaction)
                Transaction.Commit();
        }

        public void Dispose()
        {
            if (IsBaseTransaction)
                Transaction?.Dispose();

            sessionWrapper.Dispose();

            requestDataStorage.SetValue(CurrentTransactionContainerKey, parent);
        }
    }
}
