using DimasikStore.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DimasikStore.Business.Providers
{
    public interface IProvider<T>
        where T : IEntity
    {
        Task<T> GetById(string id);
        Task<T> Save(T instance);
        Task Update(T instance);
        Task Delete(T instance);
        Task Delete(string id);
    }
}
