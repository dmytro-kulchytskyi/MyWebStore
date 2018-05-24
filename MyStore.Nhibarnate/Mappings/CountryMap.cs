using FluentNHibernate.Mapping;
using MyStore.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate.Mappings
{
    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Table("Country");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.Name).Not.Nullable().Unique();
            Map(x => x.Available).Not.Nullable();
        }
    }
}
