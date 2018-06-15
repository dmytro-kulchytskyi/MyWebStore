using MyStore.Business.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate.Mappings
{
    public class BasketMap : ClassMap<Basket>
    {
        public BasketMap() 
        {
            Table("Basket");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.UserId).Not.Nullable();
        }
    }
}
