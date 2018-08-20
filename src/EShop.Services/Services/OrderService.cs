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
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<OrderProfile>();
                cfg.AddProfile<ProductProfile>();
                cfg.AddProfile<ProductOrderProfile>();
            });
        }

        public void Create(OrderDTO orderDTO)
        {
            _repository.Create(Mapper.Map<Order>(orderDTO));
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            return Mapper.Map<IEnumerable<Order>, List<OrderDTO>>(_repository.GetAll());
        }
    }
}
