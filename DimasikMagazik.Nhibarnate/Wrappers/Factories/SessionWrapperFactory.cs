using NHibernate;
using DimasikMagazik.Business;
using DimasikMagazik.Nhibarnate;

namespace DimasikMagazik.Nhibernate.Wrappers.Factories
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
