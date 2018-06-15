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
        [Display(Name = "Index in progress")]
        public bool IndexInProgress { get; set; }

        [Display(Name = "Index finished")]
        public bool IndexFinished { get; set; }

        [Display(Name = "Index success")]
        public bool IndexSuccess { get; set; }

        [Display(Name = "Index exists")]
        public bool IndexExists { get; set; }

        [Display(Name = "Index progress")]
        public double IndexProgressPercentage { get; set; }

        [Display(Name = "Error message")]
        public string IndexErrorMessage { get; set; }

        [Display(Name = "Error stack trace")]
        public string IndexErrorStackTrace { get; set; }
    }
}