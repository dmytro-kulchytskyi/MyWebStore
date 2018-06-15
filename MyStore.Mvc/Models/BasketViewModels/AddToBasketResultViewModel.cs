using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models.BasketViewModels
{
    public class AddToBasketResultViewModel
    {
        public string ProductId { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }
    }
}