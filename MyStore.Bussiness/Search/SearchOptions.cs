using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Search
{
    public class SearchOptions
    {
        //string searchQuery, IEnumerable<string> resultFields, int maxResults = 0, string sortField = null, bool orderByDesc = false, IEnumerable<string> searchFields = null
        public string Query { get; set; }

        public IEnumerable<string> ResultFields { get; set; }

        public int MaxResults { get; set; }

        public string SortField { get; set; }

        public bool InverseOrder { get; set; }

        public IEnumerable<string> SearchFields { get; set; }

    }
}
