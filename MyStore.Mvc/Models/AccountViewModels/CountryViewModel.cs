using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models.AccountViewModels
{
    public class CountryViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Country")]
        public string Name { get; set; }
    }
}