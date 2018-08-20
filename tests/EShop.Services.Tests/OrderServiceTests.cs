using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.Services;
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
        [Fact]
        public void GetOrders_Invoke_ReturnOrdersCollection()
        {
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.GetAll()).Returns(GetOrders());
            var service = new OrderService(mock.Object);

            var result = service.GetOrders();

            Assert.Equal(5, result.Count());
        }

        private IQueryable<Order> GetOrders()
        {
            var product = new Product { ProductId = 1, Name = "P1", Description = "Des1", Count = 2 };
            List<Order> orders = new List<Order>
            {
                new Order { OrderId = 1,
                    Products = new List<Product>
                    {
                        product
                    }
                },
                new Order { OrderId = 2,
                Products = new List<Product>
                    {
                       product
                    }
                },
                new Order { OrderId = 3,
                Products = new List<Product>
                    {
                        product
                    }
                },
                new Order { OrderId = 4,
                Products = new List<Product>
                    {
                        product
                    }
                },
                new Order { OrderId = 5,
                Products = new List<Product>
                    {
                        product
                    }
                }
            };

            return orders.AsQueryable();
        }
    }
}
