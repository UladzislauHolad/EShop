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
using System.Text;
using Xunit;

namespace EShop.Services.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public void GetCustomers_Invoke_CustomersReturned()
        {
            var customers = new List<Customer>
            {
                new Customer(),
                new Customer()
            };

            var repository = new Mock<IRepository<Customer>>();
            repository.Setup(repo => repo.GetAll()).Returns(customers.AsQueryable());
            var service = new СustomerService(repository.Object, GetMapper());

            var result = service.GetCustomers();

            Assert.True(result is IEnumerable<CustomerDTO>);
            Assert.Equal(customers.Count, result.Count());
        }

        [Fact]
        public void Create_Invoke_CustomerCreated()
        {
            var customer = new Customer { FirstName = "Cust1" };
            var customerDTO = new CustomerDTO { FirstName = "Cust1" };
            var repository = new Mock<IRepository<Customer>>();
            repository.Setup(repo => repo.Create(customer));
            var service = new СustomerService(repository.Object, GetMapper());

            service.Create(customerDTO);

            repository.Verify(m => m.Create(It.Is<Customer>(c => c.FirstName == "Cust1")), Times.Once);
        }

        [Fact]
        public void GetCustomer_Invoke_CustomerReturned()
        {
            var customer = new Customer { CustomerId = 1, FirstName = "Cust1" };
            var repository = new Mock<IRepository<Customer>>();
            repository.Setup(repo => repo.Get(1)).Returns(customer);
            var service = new СustomerService(repository.Object, GetMapper());

            var result = service.GetCustomer(1);

            Assert.True(result is CustomerDTO);
            repository.Verify(m => m.Get(1), Times.Once);
        }

        [Fact]
        public void Update_Invoke_CustomerUpdated()
        {
            var customer = new Customer { CustomerId = 1, FirstName = "Cust1" };
            var customerDTO = new CustomerDTO { CustomerId = 1, FirstName = "Cust1" };
            var repository = new Mock<IRepository<Customer>>();
            repository.Setup(repo => repo.Update(customer));
            var service = new СustomerService(repository.Object, GetMapper());

            service.Update(customerDTO);

            repository.Verify(m => m.Update(It.Is<Customer>(c => c.FirstName == "Cust1")), Times.Once);
        }

        [Fact]
        public void Delete_InvokeWithId_CustomerDeleted()
        {
            var repository = new Mock<IRepository<Customer>>();
            repository.Setup(repo => repo.Delete(1));
            var service = new СustomerService(repository.Object, GetMapper());

            service.Delete(1);

            repository.Verify(m => m.Delete(1), Times.Once);
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new CustomerDTOProfile())
            ).CreateMapper();

            return mapper;
        }
    }
}
