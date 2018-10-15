using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Infrastructure;
using EShop.Services.Infrastructure.Enums;
using EShop.Services.Infrastructure.Exceptions;
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
        IRepository<Order> _orderRepository;
        IRepository<Customer> _customerRepository;
        IMapper _mapper;

        public OrderService(IRepository<Order> orderRepository, IRepository<Customer> customerRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public OrderDTO Create(OrderDTO orderDTO)
        {
            var existCustomer = _mapper.Map<CustomerDTO>(_customerRepository.Get(orderDTO.CustomerId));

            if (!orderDTO.Customer.Equals(existCustomer))
                orderDTO.CustomerId = 0;
            else
                orderDTO.Customer = null;

            if (orderDTO.PickupPointId == 0)
                orderDTO.PickupPointId = null;

            var order = _mapper.Map<Order>(orderDTO);
            AddStatusChanges(order, StatusStates.New.ToString());

            return _mapper.Map<OrderDTO>(_orderRepository.Create(order));
        }

        public void Delete(int id)
        {
            var order = _orderRepository.Get(id);
            if (order != null)
            {
                Enum.TryParse(order.Status, out StatusStates statusState);
                StatusState status = new StatusState(statusState);
                try
                {
                    order.Status = status.GetNext(Commands.Delete).ToString();
                    AddStatusChanges(order, order.Status);
                    _orderRepository.Update(order);
                }
                catch (InvalidOperationException ex)
                {
                    throw ex;
                }
            }
            else
                throw new InvalidOperationException("This order is not exist");
        }

        public OrderDTO GetOrder(int id)
        {
            return _mapper.Map<OrderDTO>(_orderRepository.Get(id));
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            var orders = _mapper.Map<IEnumerable<Order>, List<OrderDTO>>(_orderRepository.GetAll().Where(o => o.Status != StatusStates.Deleted.ToString()));
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

        public void Update(int id, OrderDTO orderDTO)
        {
            var existOrder = _orderRepository.Get(id);
            if(existOrder != null)
            {
                orderDTO.OrderId = id;
                var existCustomer = _mapper.Map<CustomerDTO>(_customerRepository.Get(orderDTO.CustomerId));

                if (!orderDTO.Customer.Equals(existCustomer))
                    orderDTO.CustomerId = 0;
                else
                    orderDTO.Customer = null;

                if (orderDTO.PickupPointId == 0)
                    orderDTO.PickupPointId = null;

                _orderRepository.Update(_mapper.Map<Order>(orderDTO));
            }
            else
                throw new EntityNotExistException($"Order with id {id} is not exist");
        }

        private void AddStatusChanges(Order order, string status)
        {
            var date = DateTime.Now;
            order.Date = date;
            order.OrderStatusChanges.Add(new OrderStatusChange { Status = status, Date = date });
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

        public object GetCountOfConfirmedProducts()//критерий передавать в паараметре
        {
            var orders = _orderRepository.GetAll();
            var products = orders.Where(o => o.Status == "Confirmed")
                .SelectMany(o => o.ProductOrders)
                .GroupBy(pc => pc.Name)
                .Select(g => new { Name = g.Key, Count = g.Select(p => p.OrderCount).Sum() });
            //поменять object
            return products;
        }

        public object GetCountOfConfirmedOrdersByDate()
        {
            var orders = _orderRepository.GetAll()
                .Where(o => o.Status == "Paid")
                .GroupBy(o => o.Date.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() });
            
            return orders;
        }

        public OrderDTO ChangeState(int id)
        {
            var order = _orderRepository.Get(id);
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
                    AddStatusChanges(order, order.Status);

                    return _mapper.Map<OrderDTO>(_orderRepository.Update(order));
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
