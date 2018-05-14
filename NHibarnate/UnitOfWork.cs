﻿using DimasikStore.Business;
using DimasikStore.Nhibernate.Wrappers;
using DimasikStore.Nhibernate.Wrappers.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimasikStore.Nhibarnate
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

        public async Task Commit()
        {
            try
            {
                await transaction.Commit();
            }
            catch
            {
                await Rollback();
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public async Task Rollback()
        {
            try
            {
                await transaction.Rollback();
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