using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models.BasketViewModels
{
    public class BasketViewModel
    {
        public IList<BasketProductViewModel> Products { get; set; }

        public decimal TotalPrice { get; set; }
    }
}