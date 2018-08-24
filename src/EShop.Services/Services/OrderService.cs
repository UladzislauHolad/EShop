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
    public class OrderService : IOrderService
    {
        IRepository<Order> _repository;

        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public void Create(OrderDTO orderDTO)
        {
            var mapper = GetMapper();

            _repository.Create(mapper.Map<Order>(orderDTO));
        }

        public void Delete(int id)
        {
            var mapper = GetMapper();
            _repository.Delete(id);
        }

        public OrderDTO GetOrder(int id)
        {
            var mapper = GetMapper();
            return mapper.Map<OrderDTO>(_repository.Get(id));
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            var mapper = GetMapper();

            return mapper.Map<IEnumerable<Order>, List<OrderDTO>>(_repository.GetAll());
        }

        public void Update(OrderDTO orderDTO)
        {
            var mapper = GetMapper();

            _repository.Update(mapper.Map<Order>(orderDTO));
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
    }
}
