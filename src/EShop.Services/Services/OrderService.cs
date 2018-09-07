using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Infrastructure;
using EShop.Services.Infrastructure.Enums;
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

            var orders = mapper.Map<IEnumerable<Order>, List<OrderDTO>>(_repository.GetAll());
            var nextActionSetter = new NextActionState();

            foreach (var order in orders)
            {
                if (IsConfirmAvailable(order))
                {
                    Enum.TryParse(order.Status, out StatusStates currentState);
                    Enum.TryParse(order.PaymentMethod.Name, out PaymentMethods paymentMethod);
                    Enum.TryParse(order.DeliveryMethod.Name, out DeliveryMethods deliveryMethod);
                    order.Command = nextActionSetter.GetNext(currentState, paymentMethod, deliveryMethod);
                }
                else
                {
                    order.Command = Commands.Nothing;
                }
            }

            return orders;
        }

        public void Update(OrderDTO orderDTO)
        {
            var mapper = GetMapper();

            _repository.Update(mapper.Map<Order>(orderDTO));
        }

        private bool IsConfirmAvailable(OrderDTO order)
        {
            bool result = true;
           
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

            return result;
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrderProfile());
                cfg.AddProfile(new ProductOrderProfile());
                cfg.AddProfile(new ProductProfile());
                cfg.AddProfile(new PaymentMethodDTOProfile());
            }).CreateMapper();

            return mapper;
        }

        public object GetCountOfConfirmedProducts()//критерий передавать в паараметре
        {
            var orders = _repository.GetAll();
            var products = orders.Where(o => o.Status == "Confirmed")
                .SelectMany(o => o.ProductOrders)
                .GroupBy(pc => pc.Name)
                .Select(g => new { Name = g.Key, Count = g.Select(p => p.OrderCount).Sum() });
            //поменять object
            return products;
        }

        public object GetCountOfConfirmedOrdersByDate()
        {
            var orders = _repository.GetAll()
                .Where(o => o.Status == "Paid")
                .GroupBy(o => o.Date.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() });
            
            return orders;
        }

        public void ChangeState(int id)
        {
            var order = _repository.Get(id);
            if(order != null)
            {
                Enum.TryParse(order.Status, out StatusStates statusState);
                Enum.TryParse(order.PaymentMethod.Name, out PaymentMethods paymentMethod);
                Enum.TryParse(order.DeliveryMethod.Name, out DeliveryMethods deliveryMethod);
                NextActionState actionState = new NextActionState();
                StatusState status = new StatusState(statusState);
                try
                {
                    Commands nextAction = actionState.GetNext(statusState, paymentMethod, deliveryMethod);
                    order.Status = status.GetNext(nextAction).ToString();
                    _repository.Update(order);
                }
                catch(InvalidOperationException ex)
                {
                    throw ex;
                }
            }
            else
                throw new InvalidOperationException("This order is not exist");
        }
    }
}
