using MyStore.Business.Providers;
using MyStore.Business.Search;
using MyStore.Nhibernate.Wrappers.Factories;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate.Providers
{
    public class SearchIndexInfoProvider : Provider<SearchIndexInfo>, ISearchIndexInfoProvider
    {
        private ISession session;

        private ITransaction transaction;

        public SearchIndexInfoProvider(SessionWrapperFactory sessionWrapperFactory, TransactionWrapperFactory transactionWrapperFactory)
            : base(sessionWrapperFactory, transactionWrapperFactory)
        {
        }

        public SearchIndexInfo GetCurrentSearchIndexInfo()
        {
            return providerHelper.Invoke(s => s.QueryOver<SearchIndexInfo>().OrderBy(it => it.Date).Desc.Where(it => it.IndexFinished && it.IndexSuccess).Take(1).SingleOrDefault());
        }

        public void SaveCurrentSearchIndexInfo(SearchIndexInfo searchIndexInfo)
        {
            if (session == null || transaction == null)
                throw new InvalidOperationException("You must call EnterLock() before");

            searchIndexInfo.Id = searchIndexInfo.Id ?? Guid.NewGuid().ToString();

            session.Save(searchIndexInfo);
        }

        public void EnterLock()
        {
            try
            {
                session = sessionWrapperFactory.Create().Session;

                transaction = session.BeginTransaction(System.Data.IsolationLevel.Serializable);
            }
            catch
            {
                ExitLock();
                throw;
            }

        }

        public void ExitLock()
        {
            try
            {
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
                session.Dispose();
            }
        }
    }
}
