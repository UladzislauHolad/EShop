using AutoMapper;
using EShop.App.Web.Models.Angular.Product;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles.Angular.Product
{
    public class ProductAngularViewModelProfile : Profile
    {
        public ProductAngularViewModelProfile()
        {
            CreateMap<ProductDTO, ProductAngularViewModel>();
            CreateMap<ProductAngularViewModel, ProductDTO>();
        }
    }
}
