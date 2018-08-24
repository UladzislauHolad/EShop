using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;

namespace EShop.App.Web
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDTO, ProductViewModel>();
            CreateMap<ProductViewModel, ProductDTO>();
        }
    }
}
