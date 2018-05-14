﻿using DimasikMagazik.Mvc.App_Start.Unity;
using DimasikMagazik.Mvc.EntityMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DimasikMagazik
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

            MapperConfig.Initialize();
        }
    }
}