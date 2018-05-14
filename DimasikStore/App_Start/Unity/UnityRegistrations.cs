using DimasikStore.Business;
using DimasikStore.Business.Providers;
using DimasikStore.Mvc.Identity;
using DimasikStore.Nhibarnate;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using Unity.Injection;

namespace DimasikStore.Mvc.App_Start.Unity
{
    public static class UnityRegistrations
    {
        public static void RegisterTypes(IUnityContainer c)
        {
            c.RegisterType<Microsoft.AspNet.Identity.UserManager<IdentityUser, string>, Identity.Managers.IdentityUserManager>();
            c.RegisterType<Microsoft.AspNet.Identity.Owin.SignInManager<IdentityUser, string>, Identity.Managers.IdentitySignInManager>();
            c.RegisterType<IAuthenticationManager, IAuthenticationManager>(new InjectionFactory((_c, type, name) => HttpContext.Current.GetOwinContext().Authentication));
            c.RegisterType<Microsoft.AspNet.Identity.IUserStore<IdentityUser, string>, Identity.Stores.IdentityUserStore>();

            c.RegisterType<IRequestDataStorage, RequestDataStorage>();

            c.RegisterType<IUserProvider, Nhibarnate.Providers.UserProvider>();
            c.RegisterType<IProductProvider, Nhibarnate.Providers.ProductProvider>();
        }
    }
}