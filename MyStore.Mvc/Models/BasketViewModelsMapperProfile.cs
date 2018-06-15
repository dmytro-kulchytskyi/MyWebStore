using AutoMapper;
using MyStore.Business.Entities;
using MyStore.Business.Search;
using MyStore.Mvc.Models.AdminViewModels;
using MyStore.Mvc.Models.BasketViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models
{
    public class BasketViewModelsMapperProfile : Profile
    {
        public BasketViewModelsMapperProfile()
        {
            CreateMap<Product, BasketProductViewModel>()
                .ForMember(p => p.Url, m => m.MapFrom(s => UrlHelper.GetUrlSafeLine(s.Title)))
                .ForMember(p => p.InStock, m => m.MapFrom(s => s.Stock > 0));

        }
    }
}