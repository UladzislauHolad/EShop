using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using EShop.Services.Profiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Services
{
    public class СustomerService : ICustomerService
    {
        IRepository<Customer> _repository;

        public СustomerService(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public void Create(CustomerDTO customerDTO)
        {
            var mapper = GetMapper();

            _repository.Create(mapper.Map<Customer>(customerDTO));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public CustomerDTO GetCustomer(int id)
        {
            var mapper = GetMapper();

            return mapper.Map<CustomerDTO>(_repository.Get(id));
        }

        public IEnumerable<CustomerDTO> GetCustomers()
        {
            var mapper = GetMapper();
            
            return mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(_repository.GetAll());
        }

        public void Update(CustomerDTO customerDTO)
        {
            var mapper = GetMapper();

            _repository.Update(mapper.Map<Customer>(customerDTO));
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
