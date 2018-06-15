using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Search
{
    public interface ISearchIndexInfoProvider
    {
        SearchIndexInfo GetCurrentSearchIndexInfo();
        void SaveCurrentSearchIndexInfo(SearchIndexInfo searchIndexInfo);
        void EnterLock();
        void ExitLock();
    }
}
