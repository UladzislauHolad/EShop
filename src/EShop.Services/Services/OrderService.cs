using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using EShop.Services.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Confirm(int id)
        {
            var order = _repository.Get(id);
            if(order != null)
            {
                order.IsConfirmed = true;
                order.Date = DateTime.Now;
                _repository.Update(order);
            }
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

        public bool IsConfirmAvailable(int id)
        {
            bool result = true;
            var order = _repository.Get(id);
            if(order != null)
            {
                if(order.ProductOrders != null)
                {
                    bool haveDeletedOrder = order.ProductOrders.Any(po => po.Product.IsDeleted == true);
                    bool haveNoProductOrders = order.ProductOrders.Count == 0;
                    if (haveDeletedOrder || haveNoProductOrders)
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }

            return result;
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

        public object GetCountOfConfirmedProducts()
        {
            var orders = _repository.GetAll();
            var products = orders.Where(o => o.IsConfirmed)
                .SelectMany(o => o.ProductOrders)
                .GroupBy(pc => pc.Name)
                .Select(g => new { Name = g.Key, Count = g.Select(p => p.OrderCount).Sum() });

            return products;
        }

        public object GetCountOfConfirmedOrdersByDate()
        {
            var orders = _repository.GetAll()
                .Where(o => o.IsConfirmed)
                .GroupBy(o => o.Date.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() });
            
            return orders;
        }
    }
}
