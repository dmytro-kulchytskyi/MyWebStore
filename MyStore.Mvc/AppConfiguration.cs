using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MyStore.Mvc
{
    public static class AppConfiguration
    {
        public static string LineOwerflowEnding => ConfigurationManager.AppSettings["LineOwerflowEnding"];

        public static int TopProductsCount => int.Parse(ConfigurationManager.AppSettings["TopProductsCount"]);
        
        public static int DefaultPageSize => int.Parse(ConfigurationManager.AppSettings["ProductsOnPage"]);

        public static string DefaulOrderField => ConfigurationManager.AppSettings["DefaulOrderField"];

        public static string[] AvailableProductSortFields =>
            ConfigurationManager.AppSettings["AvailableProductSortFields"]
            .Split(',')
            .Select(x => x.Trim())
            .ToArray();

        public static int SearchDropdownItemsLimit => int.Parse(ConfigurationManager.AppSettings["SearchDropdownItemsLimit"]);
    }
}