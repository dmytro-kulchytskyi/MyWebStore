using MyStore.Business.Providers;
using MyStore.Business.Search;
using MyStore.Nhibernate.Wrappers.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate.Providers
{
    public class SearchIndexInfoProvider : Provider<SearchIndexInfo>, ISearchIndexInfoProvider
    {
        public SearchIndexInfoProvider(SessionWrapperFactory sessionWrapperFactory, TransactionWrapperFactory transactionWrapperFactory) 
            : base(sessionWrapperFactory, transactionWrapperFactory)
        {
        }

        public SearchIndexInfo GetCurrentSearchIndexInfo()
        {
            return providerHelper.Invoke(s => s.QueryOver<SearchIndexInfo>().OrderBy(it => it.Date).Desc.Take(1).SingleOrDefault());
        }
    }
}
