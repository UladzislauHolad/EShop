using AutoMapper;
using EShop.App.Web.Controllers;
using EShop.App.Web.Profiles;
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
    public class DeliveryMethodControllerTests
    {
        [Fact]
        public void Deliveries_Invoke_JsonResult()
        {
            var service = new Mock<IDeliveryMethodService>();
            service.Setup(m => m.GetDeliveryMethods()).Returns(new List<DeliveryMethodDTO>());
            var controller = new DeliveryMethodController(service.Object, GetMapper());

            var result = controller.Deliveries();

            Assert.True(result is JsonResult);
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DeliveryMethodViewModelProfile());
            }).CreateMapper();

            return mapper;
        }
    }
}
