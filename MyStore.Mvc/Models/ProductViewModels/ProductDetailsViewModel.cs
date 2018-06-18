using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models.ProductViewModels
{
    public class ProductDetailsViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }
        
        public string Image { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Selling Count")]
        public int SellsCount { get; set; }
        
        public bool Banned { get; set; }

        [Display(Name = "Stock")]
        public int Stock { get; set; }
        
        public string ReturnUrl { get; set; }
        
        public bool AlreadyInBasket { get; set; }
    }
}