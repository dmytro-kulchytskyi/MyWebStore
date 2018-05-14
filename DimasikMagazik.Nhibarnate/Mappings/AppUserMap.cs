﻿using DimasikMagazik.Business.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimasikMagazik.Nhibarnate.Mappings
{
    public class AppUserMap : ClassMap<AppUser>
    {
        public AppUserMap()
        {
            Table("AppUser");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.Email).Not.Nullable().UniqueKey("Email");
            Map(x => x.PasswordHash).Not.Nullable();
            Map(x => x.Banned).Not.Nullable();
        }
    }
}
