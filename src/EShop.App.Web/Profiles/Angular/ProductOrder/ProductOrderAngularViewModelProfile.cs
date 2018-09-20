using AutoMapper;
using EShop.App.Web.Models.Angular.ProductOrder;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles.Angular.ProductOrder
{
    public class ProductOrderAngularViewModelProfile : Profile
    {
        public ProductOrderAngularViewModelProfile()
        {
            CreateMap<ProductOrderAngularViewModel, ProductOrderDTO>();
            CreateMap<ProductOrderDTO, ProductOrderAngularViewModel>();
        }
    }
}
