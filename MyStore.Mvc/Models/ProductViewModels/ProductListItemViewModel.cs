using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models.ProductViewModels
{
    public class ProductListItemViewModel
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [ScaffoldColumn(false)]
        public string Image { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Added { get; set; }

        [Display(Name = "Selling count")]
        public int SellsCount { get; set; }

        [ScaffoldColumn(false)]
        public bool Banned { get; set; }

        [Display(Name = "Stock")]
        public int Stock { get; set; }

        [ScaffoldColumn(false)]
        public string Url { get; set; }
    }
}