using DimasikStore.Business.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimasikStore.Nhibarnate.Mappings
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("Product");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.Title).Not.Nullable();
            Map(x => x.Description).CustomSqlType("ntext").Not.Nullable();
            Map(x => x.Image).Nullable();
            Map(x => x.Price).Not.Nullable();
            Map(x => x.SellsCount).Not.Nullable();
        }
    }
}
