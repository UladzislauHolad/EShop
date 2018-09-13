using AutoMapper;
using EShop.Data.Entities;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Profiles
{
    public class ProductDTOProfile : Profile
    {
        public ProductDTOProfile()
        {
            CreateMap<Product, ProductDTO>()
               .ForMember(dest => dest.Categories,
                   opt => opt.MapFrom(src => src.ProductCategories));
            CreateMap<ProductDTO, Product>()
            .ForMember(dest => dest.ProductCategories,
                opt => opt.MapFrom(src => src.Categories));

            CreateMap<ProductCategory, CategoryDTO>()
            .ForMember(dest => dest.CategoryId,
                        opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CategoryDTO, ProductCategory>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
        }
    }
}
