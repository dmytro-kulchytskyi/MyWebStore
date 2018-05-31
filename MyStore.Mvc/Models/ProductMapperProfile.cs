using AutoMapper;
using MyStore.Business;
using MyStore.Business.Entities;
using MyStore.Business.Search;
using MyStore.Business.Search.Managers;
using MyStore.Mvc.Models.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MyStore.Mvc.Models.EntityMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDetailsViewModel>();

            CreateMap<Product, ProductListItemViewModel>()
               .ForMember(x => x.Url, m => m.MapFrom(a => UrlHelper.GetUrlSafeLine(a.Title)));
            
            CreateMap<Dictionary<string, string>, ProductListItemViewModel>()
               .ForMember(x => x.Title, m => m.MapFrom(a => a[ProductFields.Title]))
               .ForMember(x => x.Url, m => m.MapFrom(a => UrlHelper.GetUrlSafeLine(a[ProductFields.Title])))
               .ForMember(x => x.Image, m => m.MapFrom(a => a[ProductFields.Image]))
               .ForMember(x => x.Added, m => m.MapFrom(a => DateTime.Parse(a[ProductFields.Added])))
               .ForMember(x => x.Banned, m => m.MapFrom(a => bool.Parse(a[ProductFields.Banned])))
               .ForMember(x => x.Id, m => m.MapFrom(a => a[ProductFields.Id]))
               .ForMember(x => x.Price, m => m.MapFrom(a => double.Parse(a[ProductFields.Price])))
               .ForMember(x => x.SellsCount, m => m.MapFrom(a => int.Parse(a[ProductFields.SellsCount])))
               .ForMember(x => x.Stock, m => m.MapFrom(a => int.Parse(a[ProductFields.Stock])));
        }
    }
}