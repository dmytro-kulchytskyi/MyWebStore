using MyStore.Business.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate.Mappings
{
    public class AppUserMap : EntityMapBase<AppUser>
    {
        public AppUserMap() : base()
        {
            Table("AppUser");
            Map(x => x.Email).Not.Nullable().Unique();
            Map(x => x.PasswordHash).Not.Nullable();
            Map(x => x.Role).Not.Nullable();
            Map(x => x.Banned).Not.Nullable();
        }
    }
}
