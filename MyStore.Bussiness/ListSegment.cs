using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business
{
    public class ListSegment<T>
    {
        public IList<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
