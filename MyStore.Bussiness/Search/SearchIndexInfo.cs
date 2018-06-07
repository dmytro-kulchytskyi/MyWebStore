using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Search
{
    public class SearchIndexInfo
    {
        public virtual string Id { get; set; }

        public virtual string IndexFilesLocation { get; set; }

        public virtual bool IndexInProgress { get; set; }

        public virtual int IndexationProgressPercentage { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual bool IndexSuccess { get; set; }

        public virtual string IndexErrorMessage { get; set; }

        public virtual string IndexErrorStackTrace { get; set; }
    }
}