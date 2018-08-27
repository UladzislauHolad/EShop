using EShop.Data.EF;
using EShop.Data.Entities;
using EShop.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EShop.Data.Tests
{
    public class ProductOrderRepositoryTests
    {
        [Fact]
        public void Delete_InvokeWithId_ProductOrderDeletedProductUpdated()
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
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
                using (var context = new EShopContext(options))
                {
                    var repository = new ProductOrderRepository(context);
                    repository.Delete(1);
                }

                using (var context = new EShopContext(options))
                {
                    var deletedProductOrder = context.Set<ProductOrder>().SingleOrDefault(po => po.ProductOrderId == 1);
                    var updatedProduct = context.Set<Product>().Find(1);

                    Assert.Null(deletedProductOrder);
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
        public void Get_InvokeWithId_ProductOrderReturned()
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
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
                using (var context = new EShopContext(options))
                {
                    var repository = new ProductOrderRepository(context);
                    repository.Get(1);
                }

                using (var context = new EShopContext(options))
                {
                    var result= context.Set<ProductOrder>().SingleOrDefault(po => po.ProductOrderId == 1);

                    Assert.NotNull(result);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Update_InvokeWithValidInstance_ProductOrderUpdated()
        {
            var order = new Order
            {
                OrderId = 1,
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

            var productOrderForUpdate = new ProductOrder
            {
                ProductOrderId = 1,
                OrderId = 1,
                ProductId = 2,
                Name = "P2",
                Description = "Des2",
                Price = 1,
                OrderCount = 1,
            };

            var product = new Product
            {
                ProductId = 2,
                Name = "P2",
                Description = "Des2",
                Price = 1,
                Count = 2
            };

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
                    context.Orders.Add(order);
                    context.Products.Add(product);
                    context.SaveChanges();
                }
                using (var context = new EShopContext(options))
                {
                    var repository = new ProductOrderRepository(context);
                    repository.Update(productOrderForUpdate);
                }

                using (var context = new EShopContext(options))
                {
                    var result = context.ProductOrders.SingleOrDefault(po => po.ProductOrderId == 1);
                    var oldProduct = context.Products.SingleOrDefault(p => p.ProductId == 1);
                    var newProduct = context.Products.SingleOrDefault(p => p.ProductId == 2);

                    Assert.NotNull(result);
                    Assert.Equal("P2", result.Name);
                    Assert.Equal(2, oldProduct.Count);
                    Assert.Equal(1, newProduct.Count);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Update_UpdateSameProduct_ProductOrderUpdated()
        {
            var order = new Order
            {
                OrderId = 1,
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

            var productOrderForUpdate = new ProductOrder
            {
                ProductOrderId = 1,
                OrderId = 1,
                ProductId = 1,
                Name = "P2",
                Description = "Des2",
                Price = 1,
                OrderCount = 2
            };

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
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
                using (var context = new EShopContext(options))
                {
                    var repository = new ProductOrderRepository(context);
                    repository.Update(productOrderForUpdate);
                }

                using (var context = new EShopContext(options))
                {
                    var result = context.ProductOrders.SingleOrDefault(po => po.ProductOrderId == 1);
                    var oldProduct = context.Products.SingleOrDefault(p => p.ProductId == 1);

                    Assert.NotNull(result);
                    Assert.Equal(0, oldProduct.Count);
                    Assert.Equal("P2", result.Name);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Create_CreateNewProductOrder_NewProductOrderCreated()
        {
            var order = new Order
            {
                OrderId = 1,
            };

            var productOrder = new ProductOrder
            {
                ProductOrderId = 1,
                OrderId = 1,
                ProductId = 1,
                Name = "P2",
                Description = "Des2",
                Price = 1,
                OrderCount = 2
            };

            var product = new Product
            {
                ProductId = 1,
                Name = "P2",
                Description = "Des2",
                Price = 1,
                Count = 2
            };

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
                    context.Orders.Add(order);
                    context.Products.Add(product);
                    context.SaveChanges();
                }
                using (var context = new EShopContext(options))
                {
                    var repository = new ProductOrderRepository(context);
                    repository.Create(productOrder);
                }

                using (var context = new EShopContext(options))
                {
                    var result = context.ProductOrders.SingleOrDefault(po => po.ProductOrderId == 1);

                    Assert.NotNull(result);
                    Assert.Equal("P2", result.Name);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
