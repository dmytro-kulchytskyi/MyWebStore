using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

        [Display(Name = "Order Field")]
        public string OrderField { get; set; }

        [Display(Name = "Order type")]
        public bool InverseOrder { get; set; }

        [ScaffoldColumn(false)]
        public string DefaultOrderField { get; set; }

        [ScaffoldColumn(false)]
        public string[] AvailableOrderFields { get; set; }

        [ScaffoldColumn(false)]
        public IList<ProductListItemViewModel> Items { get; set; }

        [ScaffoldColumn(false)]
        public int PageCount => TotalItemsCount > 0 ? (int)Math.Ceiling(TotalItemsCount / (double)PageSize) : 0;
    }
}