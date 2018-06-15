using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Mvc.Models.ProductViewModels
{
    public class ProductsListViewModel
    {
        [ScaffoldColumn(false)]
        public string Query { get; set; }

        [ScaffoldColumn(false)]
        public int PageNumber { get; set; }

        [ScaffoldColumn(false)]
        public int PageSize { get; set; }

        [ScaffoldColumn(false)]
        public int TotalItemsCount { get; set; }

        [ScaffoldColumn(false)]
        public string SortField { get; set; }

        [ScaffoldColumn(false)]
        public bool InverseSort { get; set; }

        [ScaffoldColumn(false)]
        public string[] AvailableSortFields { get; set; }

        [ScaffoldColumn(false)]
        public IList<ProductListItemViewModel> Items { get; set; }

        [ScaffoldColumn(false)]
        public int PageCount => TotalItemsCount > 0 ? (int)Math.Ceiling(TotalItemsCount / (double)PageSize) : 0;
    }
}