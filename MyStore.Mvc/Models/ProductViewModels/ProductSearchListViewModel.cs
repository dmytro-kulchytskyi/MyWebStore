using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models.ProductViewModels
{
    public class ProductSearchListViewModel
    {
        public string Query { get; set; }

        public string OrderField { get; set; }

        public bool InverseOrder { get; set; }

        public string[] SearchFields { get; set; }

        public int ResultsCount { get; set; }

        [ScaffoldColumn(false)]
        public string[] AvailableOrderFields { get; set; }
        
        [ScaffoldColumn(false)]
        public IList<ProductListItemViewModel> Items { get; set; }
    }
}