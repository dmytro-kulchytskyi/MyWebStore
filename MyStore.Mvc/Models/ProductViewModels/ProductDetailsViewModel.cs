using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models.ProductViewModels
{
    public class ProductDetailsViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int SellsCount { get; set; }

        public bool Banned { get; set; }

        public int Stock { get; set; }
    }
}