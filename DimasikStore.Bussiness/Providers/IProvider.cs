using DimasilStore.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DimasilStore.Business.Providers
{
    public interface IProvider<T>
        where T : IEntity
    {
        Task<T> GetById(string id);
        Task<T> SaveOrUpdate(T instance);
        Task Delete(T instance);
        Task Delete(string id);
    }
}
