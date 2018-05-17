using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Mapping;
using MyStore.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate.Mappings
{
    public abstract class EntityMapBase<T> : ClassMap<T>
        where T : IEntity
    {
        public EntityMapBase()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
        }
    }
}
