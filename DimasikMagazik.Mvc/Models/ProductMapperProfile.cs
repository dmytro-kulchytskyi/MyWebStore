using AutoMapper;
using DimasikMagazik.Business.Entities;
using DimasikMagazik.Mvc.Models.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DimasikMagazik.Mvc.Models.EntityMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDetailsViewModel>();

            CreateMap<Product, ProductListItemViewModel>()
                .ForMember(x => x.Url, m => m.MapFrom(a => UrlHelper.GetUrlSafeLine(a.Title)));
        }
    }
}