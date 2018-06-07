﻿using MyStore.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Providers
{
    public interface IProvider<T>
        where T : IEntity
    {
        IList<T> GetById(IEnumerable<string> ids);
        int GetCount();
        ListSegment<T> GetPageOrderedBy(string fieldName, bool inverseOrder, int count, int pageNumber);
        T GetById(string id);
        T Save(T instance);
        void Update(T instance);
        void Delete(T instance);
        void Delete(string id);
    }
}
