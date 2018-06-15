using AutoMapper;
using MyStore.Business.Search;
using MyStore.Mvc.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Models
{
    public class AdminViewModelsMapperProfile : Profile
    {
        public AdminViewModelsMapperProfile()
        {
            CreateMap<SearchIndexInfo, SearchSettingsViewModel>();

        }
    }
}