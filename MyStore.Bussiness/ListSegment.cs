using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business
{
    public class ListSegment<T>
    {
        public ListSegment()
        {
            Items = new List<T>();
        }

        public ListSegment(IList<T> items, int totalCount)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));

            if (totalCount < 1)
                throw new ArgumentException($"{nameof(totalCount)} must be positive");

            TotalCount = totalCount;
        }

        public IList<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
