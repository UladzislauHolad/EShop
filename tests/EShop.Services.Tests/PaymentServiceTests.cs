using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Profiles;
using EShop.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EShop.Services.Tests
{
    public class PaymentServiceTests
    {
        [Fact]
        public void GetPaymentMethods_Invoke_ReturnIEnumerablePaymentMethodDTO()
        {
            var repository = new Mock<IRepository<PaymentMethod>>();
            repository.Setup(m => m.GetAll()).Returns(new List<PaymentMethod>().AsQueryable());
            var service = new PaymentMethodService(repository.Object, GetMapper());

            var result = service.GetPaymentMethods();

            repository.Verify(m => m.GetAll(), Times.Once);
            Assert.True(result is IEnumerable<PaymentMethodDTO>);
        }

        private IMapper GetMapper()
        {
            var _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PaymentMethodDTOProfile());
            }).CreateMapper();

            return _mapper;
        }
    }
}
