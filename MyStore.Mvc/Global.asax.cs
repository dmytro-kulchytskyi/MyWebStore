using MyStore.Mvc;
using MyStore.Mvc.App_Start.Unity;
using MyStore.Mvc.EntityMapper;
using MyStore.Mvc.Models.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MvcDependencyResolverConfig.ConfigureDependencyResolver(UnityConfig.Container);

            ModelBinders.Binders.Add(typeof(ProductsListViewModel), new ProductsListViewModelBinder());

            MapperConfig.Initialize();
        }
    }
}
