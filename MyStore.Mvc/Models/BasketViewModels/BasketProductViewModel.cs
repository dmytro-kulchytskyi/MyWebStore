using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models.BasketViewModels
{
    public class BasketProductViewModel
    {
        public virtual string Id { get; set; }

        public virtual string Title { get; set; }
        
        public virtual string Image { get; set; }

        public virtual string Description { get; set; }

        [Display(Name = "Price")]
        public virtual decimal Price { get; set; }

        public virtual int SellsCount { get; set; }

        [Display(Name = "Stock level")]
        public virtual int Stock { get; set; }

        public virtual DateTime Added { get; set; }
        
        [Display(Name = "Count")]
        public virtual int Count { get; set; }
        
        public virtual string Url { get; set; }

        public virtual bool InStock => Stock > 0;
    }
}