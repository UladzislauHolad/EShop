using AutoMapper;
using EShop.Api.Models.CategoriesViewModels;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.MappingProfiles
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<CategoryViewModel, CategoryDTO>();
            CreateMap<CategoryViewModel, CategoryDTO>().ReverseMap();
        }
    }
}
