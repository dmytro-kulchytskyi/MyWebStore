using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity.Lifetime;

namespace DimasikStore.Mvc.App_Start.Unity.LifetimeManagers
{
    public class RequestLifetimeManager : LifetimeManager
    {
        public override object GetValue(ILifetimeContainer container = null)
        {
            throw new NotImplementedException();
        }

        protected override LifetimeManager OnCreateLifetimeManager()
        {
            throw new NotImplementedException();
        }
    }
}