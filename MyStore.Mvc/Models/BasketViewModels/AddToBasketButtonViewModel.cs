using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models.BasketViewModels
{
    public class AddToBasketButtonViewModel
    {
        public string ProductId { get; set; }

        public bool IsInStock { get; set; }
    }
}