﻿using DimasikMagazik.Business.Entities;
using DimasikMagazik.Business.Providers;
using DimasikMagazik.Nhibernate.Wrappers.Factories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DimasikMagazik.Nhibarnate.Providers
{
    public class ProductProvider : Provider<Product>, IProductProvider
    {
        public ProductProvider(SessionWrapperFactory sessionWrapperFactory, TransactionWrapperFactory transactionWrapperFactory) 
            : base(sessionWrapperFactory, transactionWrapperFactory)
        {
        }

        public Task<IList<Product>> GetTopBySellsCount(int count)
        {
            if (count < 1)
                throw new ArgumentException("Count must be positive number");

            return providerHelper.Invoke(s => s.QueryOver<Product>().OrderBy(it => it.SellsCount).Desc.Take(count).ListAsync());
        }
    }
}
