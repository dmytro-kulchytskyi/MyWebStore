using DimasikStore.Mvc.App_Start.Unity.LifetimeManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using Unity.Lifetime;

namespace DimasikStore.Mvc.App_Start.Unity
{
    public static class UnityRegisterExtentions
    {
        public static IUnityContainer RegisterTypeInSingletoneScope<TInterface, TImplementation>(this IUnityContainer container)
            where TImplementation : TInterface
        {
            return container.RegisterType<TInterface, TImplementation>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer RegisterTypeInRequestScope<TInterface, TImplementation>(this IUnityContainer container)
           where TImplementation : TInterface
        {
            return container.RegisterType<TInterface, TImplementation>(new RequestLifetimeManager());
        }
    }
}