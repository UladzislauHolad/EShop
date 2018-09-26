using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;

namespace EShop.App.Web
{
    public class ProductViewModelProfile : Profile
    {
        public ProductViewModelProfile()
        {
            CreateMap<ProductDTO, ProductViewModel>();
            CreateMap<ProductViewModel, ProductDTO>();
        }
    }
}
