using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public ActionResult Update(CustomerViewModel customer)
        {
            var existCustomer = _service.GetCustomer(customer.CustomerId);
            if(existCustomer != null)
            {
                _service.Update(_mapper.Map<CustomerDTO>(customer));

                return Ok();
            }

            return BadRequest();
        }

        public ActionResult CustomersSelectList()
        {
            var customers = _service.GetCustomers()
                .Select(c => new { c.CustomerId, FullName = $"{c.FirstName} {c.LastName} {c.Patronymic}" }).ToList();

            return Ok(new SelectList(customers, "CustomerId", "FullName"));
        }

        public ActionResult CustomerJson(int id)
        {
            var customer = _service.GetCustomer(id);

            if(customer != null)
            {
                return Ok(customer);
            }

            return NotFound();
        }
    }
}
