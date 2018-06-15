using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Search
{
    public class SearchOptions
    {
        public string Query { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public IEnumerable<string> ResultFields { get; set; }
        
        public string SortField { get; set; }

        public bool InverseSort { get; set; }

        public IEnumerable<string> SearchFields { get; set; }

    }
}
