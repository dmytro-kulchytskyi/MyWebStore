using FluentNHibernate.Mapping;
using MyStore.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate.Mappings
{
    public class AddressMap : ClassMap<Address>
    {
        public AddressMap()
        {
            Table("Address");
            Id(x => x.Id).GeneratedBy.Assigned();
            HasOne(x => x.Country).Not.LazyLoad();
            Map(x => x.Street).Not.Nullable();
            Map(x => x.HouseNumber).Not.Nullable();
            Map(x => x.PostalCode).Not.Nullable();
        }
    }
}
