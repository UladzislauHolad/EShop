using AutoMapper;
using EShop.Api.Models;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.MappingProfiles
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<CategoryNestedNodeDTO, CategoryNestedNodeViewModel>();
            CreateMap<CategoryNestedNodeDTO, CategoryNestedNodeViewModel>().ReverseMap();
        }
    }
}
