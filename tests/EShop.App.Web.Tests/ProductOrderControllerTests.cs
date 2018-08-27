using AutoMapper;
using EShop.App.Web.Controllers;
using EShop.App.Web.Models;
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
    public class ProductOrderControllerTests
    {
        [Fact]
        public void Index_InvokeWithValidId_ViewResultReturned()
        {
            var order = new OrderDTO { OrderId = 1 };
            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock.Setup(m => m.GetOrder(1)).Returns(order);
            var productOrderServiceMock = new Mock<IProductOrderService>();
            var controller = new ProductOrderController(orderServiceMock.Object, productOrderServiceMock.Object, GetMapper());

            var result = controller.Index(1);

            Assert.True(result is ViewResult);
        }

        [Fact]
        public void Index_InvokeWithNotValidId_NotFoundResultReturned()
        {
            OrderDTO order = null;
            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock.Setup(m => m.GetOrder(1)).Returns(order);
            var productOrderServiceMock = new Mock<IProductOrderService>();
            var controller = new ProductOrderController(orderServiceMock.Object, productOrderServiceMock.Object, GetMapper());

            var result = controller.Index(1);

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void Create_InvokeWithValidId_ViewResultWithProductOrderInstanceReturned()
        {
            var order = new OrderDTO { OrderId = 1 };
            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock.Setup(m => m.GetOrder(1)).Returns(order);
            var productOrderServiceMock = new Mock<IProductOrderService>();
            var controller = new ProductOrderController(orderServiceMock.Object, productOrderServiceMock.Object, GetMapper());

            var result = controller.Create(1);

            Assert.True(result is ViewResult);
            Assert.True((result as ViewResult).ViewData.Model is ProductOrderViewModel);
        }

        [Fact]
        public void Create_InvokeWithNotValidId_NotFoundResultReturned()
        {
            OrderDTO order = null;
            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock.Setup(m => m.GetOrder(1)).Returns(order);
            var productOrderServiceMock = new Mock<IProductOrderService>();
            var controller = new ProductOrderController(orderServiceMock.Object, productOrderServiceMock.Object, GetMapper());

            var result = controller.Create(1);

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void Create_InvokeWithValidInstanceOfProductOrder_RedirectToIndex()
        {
            var order = new OrderDTO { OrderId = 1 };
            var productOrder = new ProductOrderViewModel { OrderId = 1 };
            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock.Setup(m => m.GetOrder(1)).Returns(order);
            var productOrderServiceMock = new Mock<IProductOrderService>();
            var controller = new ProductOrderController(orderServiceMock.Object, productOrderServiceMock.Object, GetMapper());

            var result = controller.Create(productOrder);

            Assert.True(result is RedirectToActionResult);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Create_InvokeWithNotValidInstanceOfProductOrder_NotFoundResult()
        {
            OrderDTO order = null;
            var productOrder = new ProductOrderViewModel { OrderId = 1 };
            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock.Setup(m => m.GetOrder(1)).Returns(order);
            var productOrderServiceMock = new Mock<IProductOrderService>();
            var controller = new ProductOrderController(orderServiceMock.Object, productOrderServiceMock.Object, GetMapper());

            var result = controller.Create(productOrder);

            Assert.True(result is NotFoundResult);
        }

        //[Fact]
        //public void Delete_InvokeWithValidOrderId_RedirectToIndex()
        //{
        //    var productOrder = new ProductOrderDTO { ProductOrderId = 1 };
        //    var orderServiceMock = new Mock<IOrderService>();
        //    var productOrderServiceMock = new Mock<IProductOrderService>();
        //    productOrderServiceMock.Setup(m => m.GetProductOrder(1)).Returns(productOrder);
        //    productOrderServiceMock.Setup(m => m.Delete(1));
        //    var controller = new ProductOrderController(orderServiceMock.Object, productOrderServiceMock.Object, GetMapper());

        //    var result = controller.Delete(1);

        //    Assert.True(result is RedirectToActionResult);
        //    Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        //}

        //[Fact]
        //public void Delete_InvokeWithNotValidOrderId_NotFoundResult()
        //{
        //    ProductOrderDTO productOrder = null;
        //    var orderServiceMock = new Mock<IOrderService>();
        //    var productOrderServiceMock = new Mock<IProductOrderService>();
        //    productOrderServiceMock.Setup(m => m.GetProductOrder(1)).Returns(productOrder);
        //    productOrderServiceMock.Setup(m => m.Delete(1));
        //    var controller = new ProductOrderController(orderServiceMock.Object, productOrderServiceMock.Object, GetMapper());

        //    var result = controller.Delete(1);

        //    Assert.True(result is NotFoundResult);
        //}

        //[Fact]
        //public void Edit_InvokeWithNotValidId_NotFoundResult()
        //{
        //    ProductOrderDTO productOrder = null;
        //    var orderServiceMock = new Mock<IOrderService>();
        //    var productOrderServiceMock = new Mock<IProductOrderService>();
        //    productOrderServiceMock.Setup(m => m.GetProductOrder(1)).Returns(productOrder);
        //    var controller = new ProductOrderController(orderServiceMock.Object, productOrderServiceMock.Object, GetMapper());

        //    var result = controller.Edit(1);

        //    Assert.True(result is NotFoundResult);
        //}

        //[Fact]
        //public void Edit_InvokeWithValidId_ViewResult()
        //{
        //    ProductOrderDTO productOrder = new ProductOrderDTO { ProductOrderId = 1};
        //    var orderServiceMock = new Mock<IOrderService>();
        //    var productOrderServiceMock = new Mock<IProductOrderService>();
        //    productOrderServiceMock.Setup(m => m.GetProductOrder(1)).Returns(productOrder);
        //    var controller = new ProductOrderController(orderServiceMock.Object, productOrderServiceMock.Object, GetMapper());

        //    var result = controller.Edit(1);

        //    Assert.True(result is ViewResult);
        //}

        //[Fact]
        //public void Edit_InvokeWithValidModel_RedirectToIndex()
        //{
        //    OrderDTO order = new OrderDTO { OrderId = 1 };
        //    var productOrder = new ProductOrderDTO { ProductOrderId = 1 };
        //    var productForUpdate = new ProductOrderViewModel { OrderId = 1, ProductOrderId = 1, Count = 2 };
        //    var mapper = GetMapper();
        //    var orderServiceMock = new Mock<IOrderService>();
        //    orderServiceMock.Setup(m => m.GetOrder(1)).Returns(order);
        //    var productOrderServiceMock = new Mock<IProductOrderService>();
        //    productOrderServiceMock.Setup(m => m.GetProductOrder(1)).Returns(productOrder);
        //    productOrderServiceMock.Setup(m => m.Update(mapper.Map<ProductOrderDTO>(productForUpdate)));
        //    var controller = new ProductOrderController(orderServiceMock.Object, productOrderServiceMock.Object, GetMapper());

        //    var result = controller.Edit(productForUpdate);

        //    Assert.True(result is RedirectToActionResult);
        //}

        //[Fact]
        //public void Edit_InvokeWithNotValidProductOrderId_BadRequestResult()
        //{
        //    OrderDTO order = new OrderDTO { OrderId = 1 };
        //    ProductOrderDTO productOrder = null;
        //    var productForUpdate = new ProductOrderViewModel { OrderId = 1, ProductOrderId = 1, Count = 2 };
        //    var mapper = GetMapper();
        //    var orderServiceMock = new Mock<IOrderService>();
        //    orderServiceMock.Setup(m => m.GetOrder(1)).Returns(order);
        //    var productOrderServiceMock = new Mock<IProductOrderService>();
        //    productOrderServiceMock.Setup(m => m.GetProductOrder(1)).Returns(productOrder);
        //    productOrderServiceMock.Setup(m => m.Update(mapper.Map<ProductOrderDTO>(productForUpdate)));
        //    var controller = new ProductOrderController(orderServiceMock.Object, productOrderServiceMock.Object, GetMapper());

        //    var result = controller.Edit(productForUpdate);

        //    Assert.True(result is BadRequestResult);
        //}

        //[Fact]
        //public void Edit_InvokeWithNotValidOrderId_BadRequestResult()
        //{
        //    OrderDTO order = null;
        //    var productOrder = new ProductOrderDTO { ProductOrderId = 1 };
        //    var productForUpdate = new ProductOrderViewModel { OrderId = 1, ProductOrderId = 1, Count = 2 };
        //    var mapper = GetMapper();
        //    var orderServiceMock = new Mock<IOrderService>();
        //    orderServiceMock.Setup(m => m.GetOrder(1)).Returns(order);
        //    var productOrderServiceMock = new Mock<IProductOrderService>();
        //    productOrderServiceMock.Setup(m => m.GetProductOrder(1)).Returns(productOrder);
        //    productOrderServiceMock.Setup(m => m.Update(mapper.Map<ProductOrderDTO>(productForUpdate)));
        //    var controller = new ProductOrderController(orderServiceMock.Object, productOrderServiceMock.Object, GetMapper());

        //    var result = controller.Edit(productForUpdate);

        //    Assert.True(result is BadRequestResult);
        //}

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrderProfile());
                cfg.AddProfile(new ProductOrderProfile());
                cfg.AddProfile(new ProductProfile());
            }).CreateMapper();

            return mapper;
        }
    }
}
