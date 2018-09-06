using AutoMapper;
using EShop.App.Web.Controllers;
using EShop.App.Web.Models;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShop.App.Web.Tests
{
    public class CustomerControllerTests
    {
        [Fact]
        public void Update_InvokeWithValidCustomer_OkResult()
        {
            var customerViewModel = new CustomerViewModel { CustomerId = 1 };
            var customerDTO = new CustomerDTO { CustomerId = 1 };
            var service = new Mock<ICustomerService>();
            service.Setup(m => m.GetCustomer(1)).Returns(customerDTO);
            var mapper = GetMapper();
            var controller = new CustomerController(service.Object, mapper);

            var result = controller.Update(customerViewModel);

            Assert.True(result is OkResult);
        }

        [Fact]
        public void Update_InvokeWithNotValidCustomer_BadRequestResult()
        {
            var customerViewModel = new CustomerViewModel { CustomerId = 1 };
            CustomerDTO customerDTO = null;
            var service = new Mock<ICustomerService>();
            service.Setup(m => m.GetCustomer(1)).Returns(customerDTO);
            var mapper = GetMapper();
            var controller = new CustomerController(service.Object, mapper);

            var result = controller.Update(customerViewModel);

            Assert.True(result is BadRequestResult);
        }

        [Fact]
        public void CustomersSelectList_Invoke_JsonResult()
        {
            var service = new Mock<ICustomerService>();
            service.Setup(m => m.GetCustomers()).Returns(new List<CustomerDTO>());
            var mapper = GetMapper();
            var controller = new CustomerController(service.Object, mapper);

            var result = controller.CustomersSelectList();

            Assert.True(result is JsonResult);
        }

        [Fact]
        public void CustomerJson_InvokeWithValidId_JsonResult()
        {
            int id = 1;
            var service = new Mock<ICustomerService>();
            service.Setup(m => m.GetCustomer(id)).Returns(new CustomerDTO { CustomerId = 1 });
            var mapper = GetMapper();
            var controller = new CustomerController(service.Object, mapper);

            var result = controller.CustomerJson(id);

            Assert.True(result is JsonResult);
            Assert.True((result as JsonResult).Value is CustomerDTO);
        }

        [Fact]
        public void CustomerJson_InvokeWithNotValidId_NotFoundResult()
        {
            int id = 1;
            CustomerDTO customer = null;
            var service = new Mock<ICustomerService>();
            service.Setup(m => m.GetCustomer(id)).Returns(customer);
            var mapper = GetMapper();
            var controller = new CustomerController(service.Object, mapper);

            var result = controller.CustomerJson(id);

            Assert.True(result is NotFoundResult);
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CustomerViewModel, CustomerDTO>();
                cfg.CreateMap<CustomerDTO, CustomerViewModel>();
            }).CreateMapper();

            return mapper;
        }
    }
}
