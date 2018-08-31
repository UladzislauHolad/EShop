using AutoMapper;
using EShop.App.Web.Controllers;
using EShop.App.Web.Models;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EShop.App.Web.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Index_Invoke_ReturnsNotNullResult()
        {
            var mock = new Mock<IOrderService>();
            mock.Setup(s => s.GetOrders()).Returns(GetOrders());
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Index() as ViewResult;

            Assert.NotNull(result.ViewData.Model);
        }

        [Fact]
        public void Index_Invoke_ReturnsIEnumerableOrderViewModelResult()
        {
            var mock = new Mock<IOrderService>();
            mock.Setup(s => s.GetOrders()).Returns(GetOrders());
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Index() as ViewResult;

            Assert.True(result.ViewData.Model is IEnumerable<OrderViewModel>);
        }

        [Fact]
        public void Index_Invoke_OrderHaveIEnumerableProductOrderViewModel()
        {
            var mock = new Mock<IOrderService>();
            mock.Setup(s => s.GetOrders()).Returns(GetOrders());
            var controller = new OrderController(mock.Object, GetMapper());

            var result = ((controller.Index() as ViewResult).ViewData.Model as IEnumerable<OrderViewModel>).ToArray();

            Assert.NotNull(result[0].ProductOrders);
        }

        [Fact]
        public void Create_Invoke_ViewResult()
        {
            var mock = new Mock<IOrderService>();
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Create();

            Assert.True(result is ViewResult);
        }

        [Fact]
        public void Edit_InvokeWithValidId_ViewResult()
        {
            var mock = new Mock<IOrderService>();
            mock.Setup(m => m.GetOrder(1)).Returns(new OrderDTO { IsConfirmed = false });
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Edit(1);

            mock.Verify(m => m.GetOrder(1), Times.Once);
            Assert.True(result is ViewResult);
        }

        [Fact]
        public void Edit_InvokeWithIdOfConfirmedOrder_ViewResult()
        {
            var mock = new Mock<IOrderService>();
            mock.Setup(m => m.GetOrder(1)).Returns(new OrderDTO { IsConfirmed = true });
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Edit(1);

            mock.Verify(m => m.GetOrder(1), Times.Once);
            Assert.True(result is ViewResult);
        }

        [Fact]
        public void Edit_InvokeWithNotValidId_BadRequestResult()
        {
            OrderDTO order = null;
            var mock = new Mock<IOrderService>();
            mock.Setup(m => m.GetOrder(1)).Returns(order);
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Edit(1);

            mock.Verify(m => m.GetOrder(1), Times.Once);
            Assert.True(result is BadRequestResult);
        }

        [Fact]
        public void Edit_InvokeWithValidOrder_OkResult()
        {
            OrderDTO order = new OrderDTO { OrderId = 1, IsConfirmed = false };
            OrderViewModel orderViewModel = new OrderViewModel { OrderId = 1 };
            var mock = new Mock<IOrderService>();
            mock.Setup(m => m.GetOrder(1)).Returns(order);
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Edit(1, orderViewModel);

            mock.Verify(m => m.GetOrder(1), Times.Once);
            Assert.True(result is OkResult);
        }

        [Fact]
        public void Edit_InvokeWithNotValidOrder_BadRequest()
        {
            OrderDTO order = null;
            OrderViewModel orderViewModel = new OrderViewModel { OrderId = 1 };
            var mock = new Mock<IOrderService>();
            mock.Setup(m => m.GetOrder(1)).Returns(order);
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Edit(1, orderViewModel);

            mock.Verify(m => m.GetOrder(1), Times.Once);
            Assert.True(result is BadRequestResult);
        }

        [Fact]
        public void Edit_InvokeWithConfirmedOrder_BadRequest()
        {
            OrderDTO order = new OrderDTO { OrderId = 1, IsConfirmed = true };
            OrderViewModel orderViewModel = new OrderViewModel { OrderId = 1 };
            var mock = new Mock<IOrderService>();
            mock.Setup(m => m.GetOrder(1)).Returns(order);
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Edit(1, orderViewModel);

            mock.Verify(m => m.GetOrder(1), Times.Once);
            Assert.True(result is BadRequestResult);
        }

        [Fact]
        public void Delete_InvokeWithValidId_OkResult()
        {
            var mock = new Mock<IOrderService>();
            mock.Setup(m => m.GetOrder(1)).Returns(new OrderDTO());
            mock.Setup(m => m.Delete(1));
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Delete(1);

            Assert.True(result is OkResult);
        }

        [Fact]
        public void Delete_InvokeWithNotValidId_NotFoundResult()
        {
            OrderDTO order = null;
            var mock = new Mock<IOrderService>();
            mock.Setup(m => m.GetOrder(1)).Returns(order);
            mock.Setup(m => m.Delete(1));
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Delete(1);

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void Delete_DeleteConfirmedOrder_BadRequestResult()
        {
            var order = new OrderDTO { OrderId = 1, IsConfirmed = true };
            var mock = new Mock<IOrderService>();
            mock.Setup(m => m.GetOrder(1)).Returns(order);
            mock.Setup(m => m.Delete(1));
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Delete(1);

            Assert.True(result is BadRequestResult);
        }

        [Fact]
        public void Confirm_ConfirmNotConfirmedOrder_OkResult()
        {
            var order = new OrderDTO { OrderId = 1 };
            var orderForUpdate = new OrderDTO { OrderId = 1, IsConfirmed = true };
            var mock = new Mock<IOrderService>();
            mock.Setup(m => m.GetOrder(1)).Returns(order);
            mock.Setup(m => m.Confirm(1));
            mock.Setup(m => m.IsConfirmAvailable(1)).Returns(true);
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Confirm(1);

            mock.Verify(m => m.Confirm(1), Times.Once);
            mock.Verify(m => m.IsConfirmAvailable(1), Times.Once);
            Assert.True(result is OkResult);
        }

        [Fact]
        public void Confirm_ConfirmConfirmedOrder_BadRequestResult()
        {
            var order = new OrderDTO { OrderId = 1, IsConfirmed = true };
            var orderForUpdate = new OrderDTO { OrderId = 1, IsConfirmed = true };
            var mock = new Mock<IOrderService>();
            mock.Setup(m => m.GetOrder(1)).Returns(order);
            mock.Setup(m => m.Confirm(1));
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Confirm(1);

            Assert.True(result is BadRequestResult);
        }

        [Fact]
        public void Confirm_ConfirmNotValidOrder_NotFoundResult()
        {
            OrderDTO order = null;
            var orderForUpdate = new OrderDTO { OrderId = 1, IsConfirmed = true };
            var mock = new Mock<IOrderService>();
            mock.Setup(m => m.GetOrder(1)).Returns(order);
            mock.Setup(m => m.Update(orderForUpdate));
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Confirm(1);

            Assert.True(result is NotFoundResult);
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderViewModel, OrderDTO>();
                cfg.CreateMap<OrderDTO, OrderViewModel>();

                cfg.CreateMap<ProductOrderDTO, ProductOrderViewModel>();
                cfg.CreateMap<ProductOrderViewModel, ProductOrderDTO>();

                cfg.CreateMap<ProductDTO, ProductViewModel>();
                cfg.CreateMap<ProductViewModel, ProductDTO>();
            }).CreateMapper();

            return mapper;
        }

        private IQueryable<OrderDTO> GetOrders()
        {
            var product = new ProductDTO { ProductId = 1, Name = "P1", Description = "Des1", Count = 10 };
            List<OrderDTO> orders = new List<OrderDTO>
            {
                new OrderDTO { OrderId = 1,
                    ProductOrders = new List<ProductOrderDTO>
                    {
                        new ProductOrderDTO { ProductOrderId = 1, Product = product }
                    }
                },
                new OrderDTO { OrderId = 2,
                    ProductOrders = new List<ProductOrderDTO>
                    {
                        new ProductOrderDTO { ProductOrderId = 2, Product = product }
                    }
                },
                new OrderDTO { OrderId = 3,
                    ProductOrders = new List<ProductOrderDTO>
                    {
                        new ProductOrderDTO { ProductOrderId = 3, Product = product }
                    }
                },
                new OrderDTO { OrderId = 4,
                    ProductOrders = new List<ProductOrderDTO>
                    {
                        new ProductOrderDTO { ProductOrderId = 4, Product = product }
                    }
                },
                new OrderDTO { OrderId = 5,
                    ProductOrders = new List<ProductOrderDTO>
                    {
                        new ProductOrderDTO { ProductOrderId = 5, Product = product }
                    }
                }
            };

            return orders.AsQueryable();
        }
    }
}
