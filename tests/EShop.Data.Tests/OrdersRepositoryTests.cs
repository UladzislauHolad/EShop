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

namespace EShop.Data.Tests
{
    public class OrdersRepositoryTests
    {
        [Fact]
        public async Task GetAll_Invoke_OrdersReturned()
        {
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
                    context.Orders.AddRange(GetOrders());
                    context.SaveChanges();
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new OrdersRepository(context);
                    var result = repository.GetAll();

                    Assert.Equal(5, await result.CountAsync());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        private IEnumerable<Order> GetOrders()
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

            return orders;
        }
    }
}
