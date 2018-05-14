﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DimasikMagazik.Business.Entities;

namespace DimasikMagazik.Mvc.Identity
{
    public class IdentityMapperProfile : Profile
    {
        public IdentityMapperProfile()
        {
            CreateMap<IdentityUser, AppUser>()
                .ForMember(x => x.Email, m => m.MapFrom(a => a.UserName));

            CreateMap<AppUser, IdentityUser>()
                 .ForMember(x => x.UserName, m => m.MapFrom(a => a.Email));
        }
    }
}