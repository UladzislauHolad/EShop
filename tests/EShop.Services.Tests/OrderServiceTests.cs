using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Profiles;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EShop.Services.Tests
{
    public class OrderServiceTests
    {
        public OrderServiceTests()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<OrderProfile>();
                cfg.AddProfile<ProductProfile>();
                cfg.AddProfile<ProductOrderProfile>();
            });
        }

        [Fact]
        public void GetOrders_Invoke_ReturnOrdersCollection()
        {
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.GetAll()).Returns(GetOrders());
            var service = new OrderService(mock.Object);

            var result = service.GetOrders();

            Assert.Equal(5, result.Count());
        }

        [Fact]
        public void GetOrders_Invoke_OrderHaveInstanceOfProductOrders()
        {
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.GetAll()).Returns(GetOrders());
            var service = new OrderService(mock.Object);

            var result = service.GetOrders().ToArray();

            Assert.NotNull(result[0].ProductOrders);
        }

        [Fact]
        public void Create_CreateOrder_OrderIsCreated()
        {
            var order = new Order
            {
                OrderId = 1,
                ProductOrders = new List<ProductOrder>
                {
                    new ProductOrder{ ProductId = 1, Count = 4 }
                }
            };
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Create(order));
            var service = new OrderService(mock.Object);
            var orderDto = Mapper.Map<Order ,OrderDTO>(order);
            var result = service.Create(orderDto);

            Assert.NotNull(result[0].ProductOrders);
        }

        private IQueryable<Order> GetOrders()
        {
            var product = new Product { ProductId = 1, Name = "P1", Description = "Des1", Count = 10 };
            List<Order> orders = new List<Order>
            {
                new Order { OrderId = 1,
                    ProductOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductOrderId = 1, OrderId = 1, ProductId = 1, Count = 1, Product = product }
                    }
                },
                new Order { OrderId = 2,
                    ProductOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductOrderId = 2, OrderId = 2, ProductId = 1, Count = 1, Product = product }
                    }
                },
                new Order { OrderId = 3,
                    ProductOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductOrderId = 3, OrderId = 3, ProductId = 1, Count = 1, Product = product }
                    }
                },
                new Order { OrderId = 4,
                    ProductOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductOrderId = 4, OrderId = 4, ProductId = 1, Count = 1, Product = product }
                    }
                },
                new Order { OrderId = 5,
                    ProductOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductOrderId = 5, OrderId = 5, ProductId = 1, Count = 1, Product = product }
                    }
                }
            };

            return orders.AsQueryable();
        }
    }
}
