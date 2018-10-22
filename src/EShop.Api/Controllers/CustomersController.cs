using AutoMapper;
using EShop.Api.Models.CustomersViewModels;
using EShop.Api.Models.OrdersViewModels;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("api/customers")]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<IEnumerable<CustomerViewModel>>(_service.GetCustomers()));
        }
    }
}
