using NHibernate;
using DimasikStore.Business;
using DimasikStore.Nhibarnate;

namespace DimasikStore.Nhibernate.Wrappers.Factories
{
    public class SessionWrapperFactory
    {
        private readonly IRequestDataStorage requestDataStorage;

        public SessionWrapperFactory(IRequestDataStorage requestDataStorage)
        {
            this.requestDataStorage = requestDataStorage;
        }

        public SessionWrapper Create() =>
            new SessionWrapper(requestDataStorage, Configuration.SessionFactory);

    }
}
