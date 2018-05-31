﻿using MyStore.Business.Entities;
using MyStore.Business.Providers;
using MyStore.Business.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Managers
{
    public class ProductManager : Manager<Product, IProductProvider>
    {
        public ProductManager(IProductProvider provider) : base(provider)
        {
        }


        public IList<Product> GetTop(string fieldName, int count)
        {
            return provider.GetTop(fieldName, count);
        }
    }
}
