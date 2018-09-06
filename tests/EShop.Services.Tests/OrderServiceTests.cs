
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
                    new ProductOrder{}
                }
            };
            var mapper = GetMapper();
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Create(order));
            var service = new OrderService(mock.Object);
            var orderDto = mapper.Map<Order, OrderDTO>(order);
            service.Create(orderDto);

            mock.Verify(m => m.Create(It.Is<Order>(o => o.OrderId == order.OrderId)), Times.Once);
        }



        [Fact]
        public void Pay_PayPaidOrder_ThrowInvalidOperationException()
        {
            var gotOrder = new Order
            {
                OrderId = 1,
                Status = "Paid",
            };
            var updatedOrder = new Order
            {
                OrderId = 1,
                Status = "Paid"
            };
            var mapper = GetMapper();
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Get(1)).Returns(gotOrder);
            mock.Setup(repo => repo.Update(It.IsAny<Order>()));
            var service = new OrderService(mock.Object);

            Assert.Throws<InvalidOperationException>(() => service.Pay(1));
            mock.Verify(m => m.Update(It.Is<Order>(o => o.Status == updatedOrder.Status)), Times.Never);
        }

        [Fact]
        public void Pay_PayNotConfirmedOrder_ThrowInvalidOperationException()
        {
            var gotOrder = new Order
            {
                OrderId = 1,
                Status = "New",
            };
            var updatedOrder = new Order
            {
                OrderId = 1,
                Status = "Paid"
            };
            var mapper = GetMapper();
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Get(1)).Returns(gotOrder);
            mock.Setup(repo => repo.Update(It.IsAny<Order>()));
            var service = new OrderService(mock.Object);

            Assert.Throws<InvalidOperationException>(() => service.Pay(1));
            mock.Verify(m => m.Update(It.Is<Order>(o => o.Status == updatedOrder.Status)), Times.Never);
        }

        [Fact]
        public void Pay_PayOrderWithUnknownStatus_ThrowInvalidOperationException()
        {
            var gotOrder = new Order
            {
                OrderId = 1,
                Status = "123",
            };
            var updatedOrder = new Order
            {
                OrderId = 1,
                Status = "Paid"
            };
            var mapper = GetMapper();
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Get(1)).Returns(gotOrder);
            mock.Setup(repo => repo.Update(It.IsAny<Order>()));
            var service = new OrderService(mock.Object);

            Assert.Throws<InvalidOperationException>(() => service.Pay(1));
            mock.Verify(m => m.Update(It.Is<Order>(o => o.Status == updatedOrder.Status)), Times.Never);
        }

        [Fact]
        public void Pay_PayConfirmedOrder_OrderIsPaid()
        {
            var gotOrder = new Order
            {
                OrderId = 1,
                Status = "Confirmed",
            };
            var updatedOrder = new Order
            {
                OrderId = 1,
                Status = "Paid"
            };
            var mapper = GetMapper();
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Get(1)).Returns(gotOrder);
            mock.Setup(repo => repo.Update(It.IsAny<Order>()));
            var service = new OrderService(mock.Object);

            service.Pay(1);

            mock.Verify(m => m.Update(It.Is<Order>(o => o.Status == updatedOrder.Status)), Times.Once);
        }

        [Fact]
        public void Confirm_ConfirmCashPayment_OrderIsPaid()
        {
            var gotOrder = new Order
            {
                OrderId = 1,
                PaymentMethod = new PaymentMethod
                {
                    Name = "Cash"
                }
            };
            var updatedOrder = new Order
            {
                OrderId = 1,
                Status = "Paid"
            };
            var mapper = GetMapper();
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Get(1)).Returns(gotOrder);
            mock.Setup(repo => repo.Update(It.IsAny<Order>()));
            var service = new OrderService(mock.Object);

            service.Confirm(1);

            mock.Verify(m => m.Update(It.Is<Order>(o => o.Status == updatedOrder.Status)), Times.Once);
        }

        [Fact]
        public void Confirm_ConfirmOnlinePayment_OrderIsConfirmed()
        {
            var gotOrder = new Order
            {
                OrderId = 1,
                PaymentMethod = new PaymentMethod
                {
                    Name = "Online"
                }
            };
            var updatedOrder = new Order
            {
                OrderId = 1,
                Status = "Confirmed"
            };
            var mapper = GetMapper();
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Get(1)).Returns(gotOrder);
            mock.Setup(repo => repo.Update(It.IsAny<Order>()));
            var service = new OrderService(mock.Object);

            service.Confirm(1);

            mock.Verify(m => m.Update(It.Is<Order>(o => o.Status == updatedOrder.Status)), Times.Once);
        }

        [Fact]
        public void Confirm_ConfirmNotExistOrder_OrderIsNotUpdated()
        {
            Order gotOrder = null;
            var mapper = GetMapper();
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Get(1)).Returns(gotOrder);
            mock.Setup(repo => repo.Update(It.IsAny<Order>()));
            var service = new OrderService(mock.Object);

            service.Confirm(1);

            mock.Verify(m => m.Update(It.IsAny<Order>()), Times.Never);
        }

        [Fact]
        public void IsConfirmAvailable_InvokeWithValidOrder_True()
        {
            var order = new Order
            {
                OrderId = 1,
                ProductOrders = new List<ProductOrder>
                {
                    new ProductOrder
                    {
                        Product = new Product {IsDeleted = false}
                    }
                }
            };
            var mapper = GetMapper();
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Get(1)).Returns(order);
            var service = new OrderService(mock.Object);

            var result = service.IsConfirmAvailable(1);

            Assert.True(result);
        }

        [Fact]
        public void IsConfirmAvailable_InvokeWithOrderWithDeletedProduct_False()
        {
            var order = new Order
            {
                OrderId = 1,
                ProductOrders = new List<ProductOrder>
                {
                    new ProductOrder
                    {
                        Product = new Product {IsDeleted = true}
                    }
                }
            };
            var mapper = GetMapper();
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Get(1)).Returns(order);
            var service = new OrderService(mock.Object);

            var result = service.IsConfirmAvailable(1);

            Assert.False(result);
        }

        [Fact]
        public void IsConfirmAvailable_InvokeWithOrderWithoutProductOrders_False()
        {
            var order = new Order
            {
                OrderId = 1,
                ProductOrders = new List<ProductOrder>()
            };
            var mapper = GetMapper();
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Get(1)).Returns(order);
            var service = new OrderService(mock.Object);

            var result = service.IsConfirmAvailable(1);

            Assert.False(result);
        }

        [Fact]
        public void IsConfirmAvailable_InvokeWithOrderWithNullProductOrders_False()
        {
            var order = new Order
            {
                OrderId = 1,
                ProductOrders = null
            };
            var mapper = GetMapper();
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Get(1)).Returns(order);
            var service = new OrderService(mock.Object);

            var result = service.IsConfirmAvailable(1);

            Assert.False(result);
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
            var service = new OrderService(mock.Object);
            var mapper = GetMapper();

            service.Update(mapper.Map<OrderDTO>(order));

            mock.Verify(m => m.Update(It.Is<Order>(o => o.OrderId == order.OrderId)), Times.Once());
        }

        [Fact]
        public void Delete_InvokeWithValidId_OrderDeleted()
        {
            var mock = new Mock<IRepository<Order>>();
            mock.Setup(repo => repo.Delete(1));
            var service = new OrderService(mock.Object);

            service.Delete(1);

            mock.Verify(m => m.Delete(1));
        }

        [Fact]
        public void GetOrder_InvokeWithValidId_ReturnOrderDTO()
        {
            int id = 1;
            var repository = new Mock<IRepository<Order>>();
            repository.Setup(m => m.Get(id)).Returns(new Order());
            var service = new OrderService(repository.Object);

            var result = service.GetOrder(id);

            Assert.True(result is OrderDTO);
        }

        [Fact]
        public void GetCountOfConfirmed_Invoke_NotNull()
        {
            var repository = new Mock<IRepository<Order>>();
            repository.Setup(m => m.GetAll()).Returns(new List<Order>().AsQueryable());
            var service = new OrderService(repository.Object);

            var result = service.GetCountOfConfirmedProducts();

            Assert.NotNull(result);
        }

        [Fact]
        public void GetCountOfConfirmedOrdersByDate_Invoke_NotNull()
        {
            var repository = new Mock<IRepository<Order>>();
            repository.Setup(m => m.GetAll()).Returns(new List<Order>().AsQueryable());
            var service = new OrderService(repository.Object);

            var result = service.GetCountOfConfirmedOrdersByDate();

            Assert.NotNull(result);
        }

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
