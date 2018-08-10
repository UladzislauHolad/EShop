using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;

namespace EShop.App.Web
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<ProductDTO, ProductViewModel>();
            CreateMap<ProductViewModel, ProductDTO>();
        }
    }
}
