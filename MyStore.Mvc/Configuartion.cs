using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MyStore
{
    public partial class AppConfiguartion
    {
        public static int TopProductsCount => int.Parse(ConfigurationManager.AppSettings["TopProductsCount"]);
        public static int ProductsOnPage => int.Parse(ConfigurationManager.AppSettings["ProductsOnPage"]);
        public static int SearchResultsCount => int.Parse(ConfigurationManager.AppSettings["SearchResultsCount"]);
    }
}