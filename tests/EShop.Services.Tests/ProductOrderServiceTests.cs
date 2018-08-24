using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Profiles;
using EShop.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShop.Services.Tests
{
    public class ProductOrderServiceTests
    {
        [Fact]
        public void Delete_InvokeId_ProductOrderDeleted()
        {
            var mock = new Mock<IRepository<ProductOrder>>();
            mock.Setup(repo => repo.Delete(1));
            var service = new ProductOrderService(mock.Object);

            service.Delete(1);

            mock.Verify(m => m.Delete(1));
        }

        [Fact]
        public void GetProductOrder_InvokeWithValidId_ProductOrderReturned()
        {
            var mock = new Mock<IRepository<ProductOrder>>();
            mock.Setup(repo => repo.Get(1));
            var service = new ProductOrderService(mock.Object);

            service.GetProductOrder(1);

            mock.Verify(m => m.Get(1));
        }

        [Fact]
        public void Update_InvokeWithValidInstance_ProductUpdated()
        {
            var productOrder = new ProductOrder { ProductOrderId = 1 };
            var mapper = GetMapper();
            var mock = new Mock<IRepository<ProductOrder>>();
            mock.Setup(repo => repo.Update(productOrder));    
            var service = new ProductOrderService(mock.Object);

            service.Update(mapper.Map<ProductOrderDTO>(productOrder));

            mock.Verify(m => m.Update(It.Is<ProductOrder>(po => po.ProductOrderId == 1)), Times.Once);
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductOrderProfile());
            }).CreateMapper();

            return mapper;
        }
    }
}
