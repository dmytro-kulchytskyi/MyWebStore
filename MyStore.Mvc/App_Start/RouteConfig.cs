using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "ProductsList",
               url: "Products/{pageNumber}",
               defaults: new { controller = "Product", action = "List", pageNumber = 1 },
               constraints: new { pageNumber = @"\d+" }
            );

            routes.MapRoute(
            name: "SearchPage",
            url: "SearchPage/{pageNumber}",
            defaults: new { controller = "Product", action = "SearchPage", pageNumber = 1 },
            constraints: new { pageNumber = @"\d+" }
         );


            routes.MapRoute(
               name: "ProductDetails",
               url: "Product/{link}_{id}",
               defaults: new { controller = "Product", action = "Details" },
               constraints: new { link = @"[A-Za-zА-Яа-яі0-9\-]+", id = @"[A-Za-zА-Яа-яі0-9\-]+" }
            );
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
