using MyStore.Business.Entities;
using MyStore.Business.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Managers
{
    public abstract class Manager<T, TProvider>
        where T : IEntity
        where TProvider : IProvider<T>
    {
        protected readonly TProvider provider;

        public Manager(TProvider provider)
        {
            this.provider = provider;
        }

        public virtual T GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"{nameof(id)} can't be empty");

            return provider.GetById(id);
        }

        public virtual int GetCount()
        {
            return provider.GetCount();
        }

        public virtual IList<T> GetPage(int pageSize, int pageNumber)
        {
            return provider.GetPage(pageSize, pageNumber);
        }
    }
}
