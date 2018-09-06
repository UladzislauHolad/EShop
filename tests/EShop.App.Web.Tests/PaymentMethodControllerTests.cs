using AutoMapper;
using EShop.App.Web.Profiles;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using EShop.App.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EShop.App.Web.Tests
{
    public class PaymentMethodControllerTests
    {
        [Fact]
        public void Payments_Invoke_ReturnJsonResult()
        {
            var service = new Mock<IPaymentMethodService>();
            service.Setup(m => m.GetPaymentMethods()).Returns(new List<PaymentMethodDTO>());
            var controller = new PaymentMethodController(service.Object, GetMapper());

            var result = controller.Payments();

            Assert.True(result is JsonResult);
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PaymentMethodViewModelProfile());
            }).CreateMapper();

            return mapper;
        }
    }
}
