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

            CreateMap<SearchResult, ProductListItemViewModel>()
               .ForMember(x => x.Title, m => m.MapFrom(a => a.GetValue(ProductFields.Title)))
               .ForMember(x => x.Url, m => m.MapFrom(a => UrlHelper.GetUrlSafeLine(a.GetValue(ProductFields.Title))))
               .ForMember(x => x.Image, m => m.MapFrom(a => a.GetValue(ProductFields.Image)))
               .ForMember(x => x.Added, m => m.MapFrom(a => DateTime.Parse(a.GetValue(ProductFields.Added))))
               .ForMember(x => x.Banned, m => m.MapFrom(a => bool.Parse(a.GetValue(ProductFields.Banned))))
               .ForMember(x => x.Id, m => m.MapFrom(a => a.GetValue(ProductFields.Id)))
               .ForMember(x => x.Price, m => m.MapFrom(a => double.Parse(a.GetValue(ProductFields.Price))))
               .ForMember(x => x.SellsCount, m => m.MapFrom(a => int.Parse(a.GetValue(ProductFields.SellsCount))))
               .ForMember(x => x.Stock, m => m.MapFrom(a => int.Parse(a.GetValue(ProductFields.Stock))));
        }
    }
}