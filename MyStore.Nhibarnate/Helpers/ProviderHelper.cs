using MyStore.Nhibernate.Wrappers.Factories;
using NHibernate;
using System;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate
{
    public class ProviderHelper
    {
        private readonly SessionWrapperFactory sessionWrapperFactory;

        public ProviderHelper(SessionWrapperFactory sessionWrapperFactory)
        {
            this.sessionWrapperFactory = sessionWrapperFactory;
        }

        public TResult Invoke<TResult>(Func<ISession, TResult> func)
        {
            using (var sessionWrapper = sessionWrapperFactory.Create())
                return func(sessionWrapper.Session);
        }

        public void Invoke(Action<ISession> action)
        {
            using (var sessionWrapper = sessionWrapperFactory.Create())
                action(sessionWrapper.Session);
        }
    }
}
