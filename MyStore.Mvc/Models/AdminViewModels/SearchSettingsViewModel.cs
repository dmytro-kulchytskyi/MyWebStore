using MyStore.Business.Search.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models.AdminViewModels
{
    public class SearchSettingsViewModel
    {
        [Display(Name = "Indexation status")]
        public IndexStatus IndexStatus { get; set; }

        [Display(Name = "Index progress")]
        public double IndexProgress { get; set; }

        [Display(Name = "Error message")]
        public string ErrorMessage { get; set; }
    }
}