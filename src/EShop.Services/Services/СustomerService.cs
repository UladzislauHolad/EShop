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
        IMapper _mapper;

        public СustomerService(IRepository<Customer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(CustomerDTO customerDTO)
        {
            _repository.Create(_mapper.Map<Customer>(customerDTO));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public CustomerDTO GetCustomer(int id)
        {
            return _mapper.Map<CustomerDTO>(_repository.Get(id));
        }

        public IEnumerable<CustomerDTO> GetCustomers()
        {
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(_repository.GetAll());
        }

        public void Update(CustomerDTO customerDTO)
        {
            _repository.Update(_mapper.Map<Customer>(customerDTO));
        }
    }
}
