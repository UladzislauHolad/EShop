using AutoMapper;
using EShop.App.Web.Models.Angular.Catalog;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles.Angular.Catalog
{
    public class CategoryNestedNodeViewModelProfile : Profile
    {
        public CategoryNestedNodeViewModelProfile()
        {
            CreateMap<CategoryNestedNodeDTO, CategoryNestedNodeViewModel>();
            CreateMap<CategoryNestedNodeDTO, CategoryNestedNodeViewModel>().ReverseMap();
        }
    }
}
