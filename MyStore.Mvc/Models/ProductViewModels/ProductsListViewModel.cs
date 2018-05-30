using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models.ProductViewModels
{
    public class ProductsListViewModel
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string OrderField { get; set; }

        public bool InverseOrder { get; set; }

        [ScaffoldColumn(false)]
        public IEnumerable<string> AvailableOrderFields { get; set; }

        [ScaffoldColumn(false)]
        public IList<ProductListItemViewModel> Items { get; set; }
    }
}