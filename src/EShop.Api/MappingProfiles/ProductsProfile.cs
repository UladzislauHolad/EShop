using AutoMapper;
using EShop.Api.Models.ProductsViewModels;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.MappingProfiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<ProductViewModel, ProductDTO>();
            CreateMap<ProductViewModel, ProductDTO>().ReverseMap();

            CreateMap<ProductTableViewModel, ProductDTO>();
            CreateMap<ProductTableViewModel, ProductDTO>().ReverseMap();

            CreateMap<CreateProductViewModel, ProductDTO>();

            CreateMap<UpdateProductViewModel, ProductDTO>();
        }
    }
}
