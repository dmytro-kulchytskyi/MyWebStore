using MyStore.Nhibernate.Wrappers;
using MyStore.Nhibernate.Wrappers.Factories;
using System;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate
{
    public class UnitOfWork : IDisposable
    {
        private readonly TransactionWrapper transaction;

        public UnitOfWork(TransactionWrapperFactory transactionWrapperFactory)
        {
            transaction = transactionWrapperFactory.Create();
        }

        public bool IsActive => transaction.IsActive;

        public bool IsCommited => transaction.IsCommited;

        public bool IsRolledBack => transaction.IsRolledBack;

        public void BeginTransaction()
        {
            transaction.Begin();
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
            finally
            {
                Dispose();
            }
        }

        public void Rollback()
        {
            try
            {
                transaction.Rollback();
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            transaction.Dispose();
        }
    }
}
