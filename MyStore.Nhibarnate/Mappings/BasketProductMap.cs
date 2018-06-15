using MyStore.Business.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate.Mappings
{
    public class BasketProductMap : ClassMap<BasketProduct>
    {
        public BasketProductMap() 
        {
            Table("BasketProduct");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.BasketId).Not.Nullable();
            Map(x => x.ProductId).Not.Nullable();
            Map(x => x.Count).Not.Nullable();
        }
    }
}
