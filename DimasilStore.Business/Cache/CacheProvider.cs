using DimasilStore.Business.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace DimasilStore.Business.Cache
{
    public class CacheProvider<T>
      where T : IEntity
    {
        protected readonly string keyPrefix;
        protected readonly int cacheDuration;
        
        public CacheProvider()
        {
            cacheDuration = int.Parse(ConfigurationManager.AppSettings["CacheDuration"]);

            keyPrefix = typeof(T).FullName;
        }

        public T GetById(string id)
        {
            return (T)MemoryCache.Default.Get(keyPrefix + id);
        }

        public void SaveMany(IEnumerable<T> entities)
        {
            foreach (var e in entities)
                Save(e);
        }

        public virtual void Save(T entity)
        {
            if (entity != null)
            {
                if (string.IsNullOrEmpty(entity.Id))
                    throw new ArgumentException("Entity id required");

                MemoryCache.Default.Set(keyPrefix + entity.Id, entity, DateTime.Now.AddMinutes(cacheDuration));
            }
        }

        public virtual void Clear(string id)
        {
            var key = keyPrefix + id;

            if (MemoryCache.Default.Contains(key))
                MemoryCache.Default.Remove(key);
        }
    }
}
