
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DimasikStore.Business;
using DimasikStore.Nhibernate.Wrappers.Factories;

namespace DimasikStore.Nhibernate.Wrappers
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

        private bool IsBaseTransaction => parent == null || !parent.IsActive;

        public void Begin()
        {
            if (Transaction == null || !Transaction.IsActive)
                Transaction = IsBaseTransaction ? sessionWrapper.Session.BeginTransaction() : parent.Transaction;
        }

        public Task Rollback()
        {
            return Transaction.RollbackAsync();
        }

        public Task Commit()
        {
            if (IsBaseTransaction)
                return Transaction.CommitAsync();

            return Task.CompletedTask;
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
