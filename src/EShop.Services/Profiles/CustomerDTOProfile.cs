using AutoMapper;
using EShop.Data.Entities;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Profiles
{
    public class CustomerDTOProfile : Profile
    {
        public CustomerDTOProfile()
        {
            CreateMap<CustomerDTO, Customer>();
            CreateMap<Customer, CustomerDTO>();
        }
    }
}
