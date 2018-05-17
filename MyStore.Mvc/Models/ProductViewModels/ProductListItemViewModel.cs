using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models.ProductViewModels
{
    public class ProductListItemViewModel
    {
        public string ExternalProductId { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public DateTime Added { get; set; }

        public int SellsCount { get; set; }

        public bool Banned { get; set; }

        public int Stock { get; set; }

        public string Url { get; set; }
    }
}