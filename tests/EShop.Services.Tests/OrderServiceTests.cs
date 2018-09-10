
using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Infrastructure;
using EShop.Services.Infrastructure.Enums;
using EShop.Services.Profiles;
using EShop.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EShop.Services.Tests
{
    public class OrderServiceTests
    {
        //[Fact]
        //public void GetOrders_Invoke_ReturnOrdersCollection()
        //{
        //    var mock = new Mock<IRepository<Order>>();
        //    mock.Setup(repo => repo.GetAll()).Returns(new List<Order> {
        //        new Order
        //        {
        //            OrderId = 1,
        //            Status = StatusStates.New.ToString(),
        //            DeliveryMethod = new DeliveryMethod
        //            {
        //                Name = DeliveryMethods.Courier.ToString()
        //            },
        //            PaymentMethod = new PaymentMethod
        //            {
        //                Name = PaymentMethods.Online.ToString()
        //            }
        //        }
        //    }.AsQueryable());
        //    var service = new OrderService(mock.Object, GetMapper());

        //    var result = service.GetOrders();

        //    Assert.Equal(1, result.Count());
        //}

        //[Fact]
        //public void GetOrders_Invoke_OrderHaveInstanceOfProductOrders()
        //{
        //    var mock = new Mock<IRepository<Order>>();
        //    mock.Setup(repo => repo.GetAll()).Returns(GetOrders());
        //    var service = new OrderService(mock.Object, GetMapper());

        //    var result = service.GetOrders().ToArray();

        //    Assert.NotNull(result[0].ProductOrders);
        //}

        [Fact]
        public void Create_CreateOrder_OrderIsCreated()
        {
            var order = new Order
            {
                OrderId = 1,
                ProductOrders = new List<ProductOrder>
                {
                    new ProductOrder{}
                }
            };
            var mapper = GetMapper();
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Create(order));
            var service = new OrderService(mock.Object, GetMapper());
            var orderDto = mapper.Map<Order, OrderDTO>(order);
            service.Create(orderDto);

            mock.Verify(m => m.Create(It.Is<Order>(o => o.OrderId == order.OrderId)), Times.Once);
        }

        [Fact]
        public void Update_UpdateOrder_OrderIsUpdated()
        {
            var order = new Order
            {
                OrderId = 1,
                ProductOrders = new List<ProductOrder>
                {
                    new ProductOrder
                    {
                        OrderId = 1,
                        ProductId = 1,
                        Name = "P1",
                        Description = "Des1",
                        Price = 1,
                        OrderCount = 1,
                        Product = new Product
                        {
                            ProductId = 1,
                            Name = "P1",
                            Description = "Des1",
                            Price = 1,
                            Count = 1
                        }
                    }
                }
            };

            var mock = new Mock<IRepository<Order>>();
            mock.Setup(m => m.Update(order));
            var service = new OrderService(mock.Object, GetMapper());
            var mapper = GetMapper();

            service.Update(mapper.Map<OrderDTO>(order));

            mock.Verify(m => m.Update(It.Is<Order>(o => o.OrderId == order.OrderId)), Times.Once());
        }

        [Fact]
        public void Delete_InvokeWithValidId_OrderDeleted()
        {
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Delete(1));
            var service = new OrderService(mock.Object, GetMapper());

            service.Delete(1);

            mock.Verify(m => m.Delete(1));
        }

        [Fact]
        public void GetOrder_InvokeWithValidId_ReturnOrderDTO()
        {
            int id = 1;
            var repository = new Mock<IRepository<Order>>();
            repository.Setup(m => m.Get(id)).Returns(new Order());
            var service = new OrderService(repository.Object, GetMapper());

            var result = service.GetOrder(id);

            Assert.True(result is OrderDTO);
        }

        [Fact]
        public void GetCountOfConfirmed_Invoke_NotNull()
        {
            var repository = new Mock<IRepository<Order>>();
            repository.Setup(m => m.GetAll()).Returns(new List<Order>().AsQueryable());
            var service = new OrderService(repository.Object, GetMapper());

            var result = service.GetCountOfConfirmedProducts();

            Assert.NotNull(result);
        }

        [Fact]
        public void GetCountOfConfirmedOrdersByDate_Invoke_NotNull()
        {
            var repository = new Mock<IRepository<Order>>();
            repository.Setup(m => m.GetAll()).Returns(new List<Order>().AsQueryable());
            var service = new OrderService(repository.Object, GetMapper());

            var result = service.GetCountOfConfirmedOrdersByDate();

            Assert.NotNull(result);
        }

        //[Fact]
        //public void ChangeState_ChangeStateOfExistOrder_StateIsChanged()
        //{
        //    const int id = 1;
        //    Order gotOrder = new Order
        //    {
        //        OrderId = id,
        //        Status = "New"
        //    };
        //    var updatedOrder = new Order
        //    {
        //        OrderId = 1,
        //        Status = "Confirmed"
        //    };
        //    var repository = new Mock<IRepository<Order>>();
        //    repository.Setup(repo => repo.Get(id)).Returns(gotOrder);
        //    repository.Setup(repo => repo.Update(It.IsAny<Order>()));
        //    var service = new OrderService(repository.Object);

        //    service.ChangeState(id, Commands.Confirm);

        //    repository.Verify(m => m.Update(It.Is<Order>(o => o.Status == updatedOrder.Status)), Times.Once);
        //}

        //[Fact]
        //public void ChangeState_ChangeStateWithWrongCommand_InvalidOperationException()
        //{
        //    const int id = 1;
        //    Order gotOrder = new Order
        //    {
        //        OrderId = id,
        //        Status = "New"
        //    };
        //    var updatedOrder = new Order
        //    {
        //        OrderId = 1,
        //        Status = "Confirmed"
        //    };
        //    var repository = new Mock<IRepository<Order>>();
        //    repository.Setup(repo => repo.Get(id)).Returns(gotOrder);
        //    repository.Setup(repo => repo.Update(It.IsAny<Order>()));
        //    var service = new OrderService(repository.Object);

        //    Assert.Throws<InvalidOperationException>(() => service.ChangeState(id, Commands.Complete));
        //    repository.Verify(m => m.Update(It.Is<Order>(o => o.Status == updatedOrder.Status)), Times.Never);
        //}

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrderProfile());
                cfg.AddProfile(new ProductOrderProfile());
                cfg.AddProfile(new ProductProfile());
                cfg.AddProfile(new PaymentMethodDTOProfile());
            }).CreateMapper();

            return mapper;
        }

        private IQueryable<Order> GetOrders()
        {
            var product = new Product { ProductId = 1, Name = "P1", Description = "Des1", Count = 10 };
            List<Order> orders = new List<Order>
            {
                new Order { OrderId = 1,
                    ProductOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductOrderId = 1, OrderId = 1, Product = product }
                    }
                },
                new Order { OrderId = 2,
                    ProductOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductOrderId = 2, OrderId = 2, Product = product }
                    }
                },
                new Order { OrderId = 3,
                    ProductOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductOrderId = 3, OrderId = 3, Product = product }
                    }
                },
                new Order { OrderId = 4,
                    ProductOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductOrderId = 4, OrderId = 4, Product = product }
                    }
                },
                new Order { OrderId = 5,
                    ProductOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductOrderId = 5, OrderId = 5, Product = product }
                    }
                }
            };

            return orders.AsQueryable();
        }
    }
}
