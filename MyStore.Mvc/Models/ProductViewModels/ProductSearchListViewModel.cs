using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models.ProductViewModels
{
    public class ProductSearchListViewModel
    {
        public string SearchQuery { get; set; }

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public int TotalItemsCount { get; private set; }

        public string OrderField { get; set; }

        public bool InverseOrder { get; set; }
        
        public string[] SearchFields { get; set; }

        public string[] AvailableOrderFields { get; set; }
       
        public IList<ProductListItemViewModel> Items { get; set; }
        
        public int PageCount => TotalItemsCount > 0 ? (int)Math.Ceiling(TotalItemsCount / (double)PageSize) : 0;
        
        public int PageNumberIncremented => PageNumber + 1;
        
        public bool HasPreviousPage => PageNumber > 0;
        
        public bool HasNextPage => PageNumber < PageCount - 1;
        
        public bool IsFirstPage => PageNumber == 0;
        
        public bool IsLastPage => PageNumber >= PageCount - 1;
    }
}