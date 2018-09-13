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
    public class DeliveryMethodRepositoryTests
    {
        [Fact]
        public void GetAll_Invoke_ReturnIQueryableDeliveryMethod()
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
                    context.SaveChanges();
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new DeliveryMethodRepository(context);
                    var result = repository.GetAll();

                    Assert.True(result is IQueryable<DeliveryMethod>);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
