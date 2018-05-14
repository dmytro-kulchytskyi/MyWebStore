using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using DimasikMagazik.Business;

namespace DimasikMagazik.Nhibernate.Wrappers
{
    public class SessionWrapper : IDisposable
    {
        private const string CurrentSessionContainerKey = "CurrentSessionWrapper";

        private readonly IRequestDataStorage requestDataStorage;

        private readonly SessionWrapper parent;

        public SessionWrapper(IRequestDataStorage requestDataStorage, ISessionFactory sessionFactory)
        {
            this.requestDataStorage = requestDataStorage;

            parent = requestDataStorage.GetValue<SessionWrapper>(CurrentSessionContainerKey);
            requestDataStorage.SetValue(CurrentSessionContainerKey, this);

            Session = IsBaseSession ? sessionFactory.OpenSession() : parent.Session;
        }

        public ISession Session { get; }

        private bool IsBaseSession => parent == null;

        public void Dispose()
        {
            if (IsBaseSession)
                Session.Dispose();

            requestDataStorage.SetValue(CurrentSessionContainerKey, parent);
        }
    }
}
