using AutoMapper;
using EShop.Api.Models.ProductsViewModels;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.MappingProfiles.ProductsMappingProfiles
{
    public class ProductTableViewModelProfile : Profile
    {
        public ProductTableViewModelProfile()
        {
            CreateMap<ProductDTO, ProductTableViewModel>();
            CreateMap<ProductDTO, ProductTableViewModel>().ReverseMap();
        }
    }
}
