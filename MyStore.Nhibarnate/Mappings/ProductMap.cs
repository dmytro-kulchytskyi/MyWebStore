﻿using MyStore.Business.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate.Mappings
{
    public class ProductMap : EntityMapBase<Product>
    {
        public ProductMap() : base()
        {
            Table("Product");
            Map(x => x.ExternalProductId).Unique().Not.Nullable();
            Map(x => x.Title).Not.Nullable();
            Map(x => x.Description).CustomSqlType("ntext").Not.Nullable();
            Map(x => x.Image).Not.Nullable();
            Map(x => x.Price).Not.Nullable();
            Map(x => x.Stock).Not.Nullable();
            Map(x => x.Banned).Not.Nullable();
            Map(x => x.SellsCount).Not.Nullable();
            Map(x => x.Added).Not.Nullable();
        }
    }
}
