using DimasikMagazik.Business.Entities;
using DimasikMagazik.Business.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DimasikMagazik.Business.Managers
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

        public virtual Task<T> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException(nameof(id));

            return provider.GetById(id);
        }
    }
}
