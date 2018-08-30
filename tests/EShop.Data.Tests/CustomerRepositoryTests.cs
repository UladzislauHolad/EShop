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
    public class CustomerRepositoryTests
    {
        [Fact]
        public async Task GetAll_Invoke_CustomersReturnedAsync()
        {
            var customers = new List<Customer>
            {
                new Customer(),
                new Customer()
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
                    context.Customers.AddRange(customers);
                    context.SaveChanges();
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new CustomerRepository(context);
                    var result = repository.GetAll();

                    Assert.Equal(customers.Count, await result.CountAsync());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Get_InvokeWithValidId_FetchSameCustomer()
        {
            var customer = new Customer { CustomerId = 1, FirstName = "Cust1" };   
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
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new CustomerRepository(context);
                    var result = repository.Get(customer.CustomerId);

                    Assert.Equal(customer.FirstName, result.FirstName);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Get_InvokeWithNotValidId_FetchNull()
        {
            var customer = new Customer { CustomerId = 1, FirstName = "Cust1" };
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
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new CustomerRepository(context);
                    var result = repository.Get(2);

                    Assert.Null(result);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Update_UpdateCustomer_CustomerUpdated()
        {
            var customer = new Customer { CustomerId = 1, FirstName = "Cust1" };

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
                    context.Customers.Add(new Customer { CustomerId = 1 });
                    context.SaveChanges();
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new CustomerRepository(context);
                    repository.Update(customer);

                    var result = repository.Get(1);

                    Assert.Equal(customer.FirstName, result.FirstName);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Delete_DeleteCustomer_CustomerDeleted()
        {
            var customer = new Customer { CustomerId = 1, FirstName = "Cust1" };

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
                    context.Customers.Add(new Customer { CustomerId = 1 });
                    context.SaveChanges();
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new CustomerRepository(context);
                    repository.Delete(1);

                    var result = repository.Get(1);

                    Assert.Null(result);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
