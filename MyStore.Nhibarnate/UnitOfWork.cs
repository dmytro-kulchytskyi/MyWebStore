using MyStore.Nhibernate.Wrappers;
using MyStore.Nhibernate.Wrappers.Factories;
using System;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate
{
    public class UnitOfWork : IDisposable
    {
        private TransactionWrapper transaction;

        private readonly TransactionWrapperFactory transactionWrapperFactory;

        public UnitOfWork(TransactionWrapperFactory transactionWrapperFactory)
        {
            this.transactionWrapperFactory = transactionWrapperFactory;
        }

        public bool IsActive => transaction.IsActive;

        public bool IsCommited => transaction.IsCommited;

        public bool IsRolledBack => transaction.IsRolledBack;

        public void BeginTransaction()
        {
            if (transaction == null || transaction.IsCommited || transaction.IsRolledBack)
                (transaction = transactionWrapperFactory.Create()).Begin();
        }

        public void Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public void Dispose()
        {
            transaction?.Dispose();
        }
    }
}
