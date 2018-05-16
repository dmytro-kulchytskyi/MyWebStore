using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace MyStore.Mvc.App_Start.Unity
{
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> container =
        new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            UnityRegistrations.RegisterTypes(container);
            return container;
        });

        public static IUnityContainer Container => container.Value;
    }
}