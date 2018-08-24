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
        public void Create_Invoke_RedirectToIndexView()
        {
            var mock = new Mock<IOrderService>();
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Create() as RedirectToActionResult;

            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public void Delete_InvokeWithValidId_RedirectToIndexView()
        {
            var mock = new Mock<IOrderService>();
            mock.Setup(m => m.GetOrder(1)).Returns(new OrderDTO());
            mock.Setup(m => m.Delete(1));
            var controller = new OrderController(mock.Object, GetMapper());

            var result = controller.Delete(1);

            Assert.True(result is RedirectToActionResult);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
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
                        new ProductOrderDTO { ProductOrderId = 1, Count = 1, Product = product }
                    }
                },
                new OrderDTO { OrderId = 2,
                    ProductOrders = new List<ProductOrderDTO>
                    {
                        new ProductOrderDTO { ProductOrderId = 2, Count = 1, Product = product }
                    }
                },
                new OrderDTO { OrderId = 3,
                    ProductOrders = new List<ProductOrderDTO>
                    {
                        new ProductOrderDTO { ProductOrderId = 3, Count = 1, Product = product }
                    }
                },
                new OrderDTO { OrderId = 4,
                    ProductOrders = new List<ProductOrderDTO>
                    {
                        new ProductOrderDTO { ProductOrderId = 4, Count = 1, Product = product }
                    }
                },
                new OrderDTO { OrderId = 5,
                    ProductOrders = new List<ProductOrderDTO>
                    {
                        new ProductOrderDTO { ProductOrderId = 5, Count = 1, Product = product }
                    }
                }
            };

            return orders.AsQueryable();
        }
    }
}
