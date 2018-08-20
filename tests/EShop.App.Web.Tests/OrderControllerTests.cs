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

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderViewModel, OrderDTO>();
                cfg.CreateMap<OrderDTO, OrderViewModel>();

                cfg.CreateMap<ProductDTO, ProductViewModel>();
                cfg.CreateMap<ProductViewModel, ProductDTO>();
            }).CreateMapper();

            return mapper;
        }

        private IQueryable<OrderDTO> GetOrders()
        {
            var product = new ProductDTO { ProductId = 1, Name = "P1", Description = "Des1", Count = 2 };
            List<OrderDTO> orders = new List<OrderDTO>
            {
                new OrderDTO { OrderId = 1,
                    Products = new List<ProductDTO>
                    {
                        product
                    }
                },
                new OrderDTO { OrderId = 2,
                    Products = new List<ProductDTO>
                    {
                       product
                    }
                },
                new OrderDTO { OrderId = 3,
                    Products = new List<ProductDTO>
                    {
                        product
                    }
                },
                new OrderDTO { OrderId = 4,
                    Products = new List<ProductDTO>
                    {
                        product
                    }
                },
                new OrderDTO { OrderId = 5,
                    Products = new List<ProductDTO>
                    {
                        product
                    }
                }
            };

            return orders.AsQueryable();
        }
    }
}
