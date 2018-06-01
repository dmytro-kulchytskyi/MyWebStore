using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Mvc
{
    public class PagedList<T>
    {
        public PagedList(int pageSize, int pageNumber, int totalCount, IEnumerable<T> items)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalItemCount = totalCount;

            if (pageSize < 1)
                throw new ArgumentException($"{nameof(pageSize)} must be positive");

            if (pageNumber < 1 || pageNumber >= PageCount)
                throw new ArgumentException($"{nameof(totalCount)} must be positive and smaller than pages count");

            if (totalCount < 1)
                throw new ArgumentException($"{nameof(totalCount)} must be positive");

            if (items.Count() > pageSize)
                throw new ArgumentException($"{nameof(items)} size cant't be greater than pageSize");
        }

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public int TotalItemCount { get; private set; }

        public int PageCount => TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0;

        public int PageNumberIncremented => PageNumber + 1;

        public bool HasPreviousPage => PageNumber > 0;

        public bool HasNextPage => PageNumber < PageCount - 1;

        public bool IsFirstPage => PageNumber == 0;

        public bool IsLastPage => PageNumber >= PageCount - 1;
    }
}
