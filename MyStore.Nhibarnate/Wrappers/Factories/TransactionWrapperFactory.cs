using MyStore.Business;

namespace MyStore.Nhibernate.Wrappers.Factories
{
    public class TransactionWrapperFactory
    {
        private readonly IRequestDataStorage requestDataStorage;
    
        private readonly SessionWrapperFactory sessionWrapperFactory;

        public TransactionWrapperFactory(IRequestDataStorage requestDataStorage, SessionWrapperFactory sessionWrapperFactory)
        {
            this.requestDataStorage = requestDataStorage;
            this.sessionWrapperFactory = sessionWrapperFactory;
        }

        public TransactionWrapper Create()
        {
            return new TransactionWrapper(requestDataStorage, sessionWrapperFactory);
        }
    }
}
