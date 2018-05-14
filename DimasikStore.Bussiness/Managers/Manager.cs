using DimasilStore.Business.Entities;
using DimasilStore.Business.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DimasilStore.Business.Managers
{
    public abstract class Manager<T, TProvider>
        where T : IEntity
        where TProvider : IProvider<T>
    {
        protected readonly TProvider provider;

        protected readonly IUnitOfWorkFactory unitOfWorkFactory;

        public Manager(TProvider provider, IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.provider = provider;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public virtual Task<T> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException(nameof(id));

            return provider.GetById(id);
        }
    }
}
