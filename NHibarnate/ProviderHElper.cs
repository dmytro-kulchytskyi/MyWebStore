using DimasikStore.Nhibernate.Wrappers.Factories;
using NHibernate;
using System;
using System.Threading.Tasks;

namespace DimasikStore.Nhibarnate
{
    public class ProviderHelper
    {
        private readonly SessionWrapperFactory sessionWrapperFactory;

        public ProviderHelper(SessionWrapperFactory sessionWrapperFactory)
        {
            this.sessionWrapperFactory = sessionWrapperFactory;
        }

        public async Task<TResult> Invoke<TResult>(Func<ISession, Task<TResult>> func)
        {
            using (var sessionWrapper = sessionWrapperFactory.Create())
                return await func(sessionWrapper.Session);
        }

        public async Task Invoke(Func<ISession, Task> func)
        {
            await Invoke<object>(async s =>
            {
                await func(s);
                return null;
            });
        }        
    }
}
