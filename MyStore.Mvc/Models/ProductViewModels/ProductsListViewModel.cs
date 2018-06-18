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
        public string Query { get; set; }
        
        public int PageNumber { get; set; }
        
        [Display(Name = "Product on page")]
        public int PageSize { get; set; }
        
        public int TotalItemsCount { get; set; }
        
        public string SortField { get; set; }
        
        public bool InverseSort { get; set; }
        
        public bool RelevantSortTypeAvailable { get; set; }
        
        public IList<ProductListItemViewModel> Items { get; set; }
        
        public int PageCount => TotalItemsCount > 0 ? (int)Math.Ceiling(TotalItemsCount / (double)PageSize) : 0;
    }
}