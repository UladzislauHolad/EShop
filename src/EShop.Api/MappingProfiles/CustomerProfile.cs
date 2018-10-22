using AutoMapper;
using EShop.Api.Models.CustomersViewModels;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.MappingProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDTO, CustomerListViewModel>();
            CreateMap<CustomerDTO, CustomerListViewModel>().ReverseMap();
        }        
    }
}
