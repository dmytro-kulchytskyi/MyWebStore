﻿using DimasikMagazik.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimasikMagazik.Business.Search
{
    public interface ISearchProvider<T>
       where T : IEntity
    {
        string SearchOne(string query, string searchField = "");
        IEnumerable<string> Search(string query, int maxResults = 0, string searchField = "");
        void AddOrUpdate(T entity);
        void AddOrUpdate(IEnumerable<T> entities);
        void Clear(string id);
        void Clear(IEnumerable<string> ids);
    }
}