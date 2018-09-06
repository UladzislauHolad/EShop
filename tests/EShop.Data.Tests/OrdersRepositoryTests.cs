using EShop.Data.EF;
using EShop.Data.Entities;
using EShop.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Threading.Tasks;
using System.Linq;

namespace EShop.Data.Tests
{
    public class OrdersRepositoryTests
    {
        [Fact]
        public async Task GetAll_Invoke_OrdersReturned()
        {
            var payment = new PaymentMethod { PaymentMethodId = 1 };
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EShopContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new EShopContext(options))
                {
                    context.Database.EnsureCreated();
                    context.Set<PaymentMethod>().Add(payment);
                    context.Orders.AddRange(GetOrders());
                    context.SaveChanges();
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new OrderRepository(context);
                    var result = repository.GetAll();

                    Assert.Equal(5, await result.CountAsync());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Create_CreateNewOrder_OrderIsCreated()
        {
            var order = new Order
            {
                OrderId = 1,
                PaymentMethodId = 1
            };
            var payment = new PaymentMethod { PaymentMethodId = 1 };
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EShopContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new EShopContext(options))
                {
                    context.Database.EnsureCreated();
                    context.Set<PaymentMethod>().Add(payment);
                    context.SaveChanges();
                    var repository = new OrderRepository(context);
                    repository.Create(order);
                }

                using (var context = new EShopContext(options))
                {
                    var result = context.Set<Order>().SingleOrDefault(o => o.OrderId == order.OrderId);

                    Assert.NotNull(result);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Update_UpdateOrder_OrderUpdated()
        {
            var order = new Order
            {
                OrderId = 1,
                PaymentMethodId = 1,
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

            var product = new Product
            {
                ProductId = 1,
                Name = "P1",
                Description = "Des1",
                Price = 1,
                Count = 2
            };
            var payment = new PaymentMethod { PaymentMethodId = 1 };

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EShopContext>()
                    .UseSqlite(connection)
                    .Options;
                using (var context = new EShopContext(options))
                {
                    context.Database.EnsureCreated();
                    context.Set<PaymentMethod>().Add(payment);
                    context.Orders.Add(new Order { OrderId = 1, PaymentMethodId = 1});
                    context.Products.Add(product);
                    context.SaveChanges();
                }
                using (var context = new EShopContext(options))
                {
                    var repository = new OrderRepository(context);
                    repository.Update(order);
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new OrderRepository(context);
                    var updatedOrder = repository.GetAll().Single(o => o.OrderId == order.OrderId);
                    var updatedProduct = context.Set<Product>().Find(product.ProductId);

                    Assert.NotNull(updatedOrder.ProductOrders);
                    Assert.NotNull(updatedOrder.ProductOrders.Single(po => po.OrderId == updatedOrder.OrderId));
                    Assert.Equal(1, updatedProduct.Count);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Delete_DeleteWithValidId_OrderDeletedProductsUpdated()
        {
            var order = new Order
            {
                OrderId = 1,
                PaymentMethodId = 1,
                ProductOrders = new List<ProductOrder>
                {
                    new ProductOrder
                    {
                        ProductOrderId = 1,
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
            var payment = new PaymentMethod { PaymentMethodId = 1 };

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EShopContext>()
                    .UseSqlite(connection)
                    .Options;
                using (var context = new EShopContext(options))
                {
                    context.Database.EnsureCreated();
                    context.PaymentMethods.Add(payment);
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
                using (var context = new EShopContext(options))
                {
                    var repository = new OrderRepository(context);
                    repository.Delete(1);
                }

                using (var context = new EShopContext(options))
                {
                    var updatedProduct = context.Set<Product>().Find(1);
                    var resultOrder = context.Set<Order>().SingleOrDefault(o => o.OrderId == 1);

                    Assert.Null(resultOrder);
                    Assert.NotNull(updatedProduct);
                    Assert.Equal(2, updatedProduct.Count);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Get_InvokeWithValidId_ReturnOrder()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var payment = new PaymentMethod { PaymentMethodId = 1 };


            try
            {
                var options = new DbContextOptionsBuilder<EShopContext>()
                    .UseSqlite(connection)
                    .Options;
                using (var context = new EShopContext(options))
                {
                    context.Database.EnsureCreated();
                    context.PaymentMethods.Add(payment);
                    context.Orders.AddRange(GetOrders());
                    context.SaveChanges();
                }
                using (var context = new EShopContext(options))
                {
                    var repository = new OrderRepository(context);
                    var result = repository.Get(1);

                    Assert.True(result is Order);
                    Assert.NotNull(result.ProductOrders);
                    Assert.NotNull(result.ProductOrders.First().Product);
                    Assert.NotNull(result.Customer);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        private IEnumerable<Order> GetOrders()
        {
            var product = new Product { ProductId = 1, Name = "P1", Description = "Des1", Count = 10 };
            List<Order> orders = new List<Order>
            {
                new Order { OrderId = 1,
                    PaymentMethodId = 1,
                    ProductOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductOrderId = 1, OrderId = 1, Product = product }
                    },
                    Customer = new Customer()
                },
                new Order { OrderId = 2,
                    PaymentMethodId = 1,
                    ProductOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductOrderId = 2, OrderId = 2, Product = product }
                    }
                },
                new Order { OrderId = 3,
                    PaymentMethodId = 1,
                    ProductOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductOrderId = 3, OrderId = 3, Product = product }
                    }
                },
                new Order { OrderId = 4,
                    PaymentMethodId = 1,
                    ProductOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductOrderId = 4, OrderId = 4, Product = product }
                    }
                },
                new Order { OrderId = 5,
                    PaymentMethodId = 1,
                    ProductOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductOrderId = 5, OrderId = 5, Product = product }
                    }
                }
            };

            return orders;
        }
    }
}
