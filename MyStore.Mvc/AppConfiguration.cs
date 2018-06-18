using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MyStore.Mvc
{
    public static class AppConfiguration
    {
        public static string LineOwerflowEnding => ConfigurationManager.AppSettings["Mvc.LineOwerflowEnding"];

        public static int TopProductsCount => int.Parse(ConfigurationManager.AppSettings["Mvc.TopProducts.Count"]);
        
        public static int ProductListPageSize => int.Parse(ConfigurationManager.AppSettings["Mvc.ProductList.ProductsOnPage"]);

        public static string DefaultSortField => ConfigurationManager.AppSettings["Mvc.ProductList.DefaultSort.Field"];

        public static bool InverseDefaultSortField => bool.Parse(ConfigurationManager.AppSettings["Mvc.ProductList.DefaultSort.InverseDefaultField"]);

        public static int SearchDropdownItemsLimit => int.Parse(ConfigurationManager.AppSettings["Mvc.SearchDropdown.MaxResults"]);
    }
}