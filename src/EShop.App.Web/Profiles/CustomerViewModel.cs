using AutoMapper;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles
{
    public class CustomerViewModel : Profile
    {
        public CustomerViewModel()
        {
            CreateMap<CustomerViewModel, CustomerDTO>();
            CreateMap<CustomerDTO, CustomerViewModel> ();
        }
    }
}
