using NHibernate;
using DimasikStore.Business;

namespace DimasikStore.Nhibernate.Wrappers.Factories
{
    public class SessionWrapperFactory
    {
        private readonly IRequestDataStorage requestDataStorage;

        public SessionWrapperFactory(IRequestDataStorage requestDataStorage)
        {
            this.requestDataStorage = requestDataStorage;
        }

        public SessionWrapper Create()
        {
            return new SessionWrapper(requestDataStorage, Configuration.SessionFactory);
        }
    }
}
