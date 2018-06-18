using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models.BasketViewModels
{
    public class BasketViewModel
    {
        public IList<BasketProductViewModel> Products { get; set; }

        [Display(Name = "Total price")]
        public decimal TotalPrice { get; set; }
    }
}