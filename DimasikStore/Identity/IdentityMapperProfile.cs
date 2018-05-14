using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DimasikStore.Business.Entities;

namespace DimasikStore.Mvc.Identity
{
    public class IdentityMapperProfile : Profile
    {
        public IdentityMapperProfile()
        {
            CreateMap<IdentityUser, AppUser>();
            CreateMap<AppUser, IdentityUser>();
        }
    }
}