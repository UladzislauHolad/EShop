﻿using EShop.Services.DTO;
using EShop.Services.Infrastructure;
using EShop.Services.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetOrders();
        OrderDTO Create(OrderDTO orderDTO);
        OrderDTO GetOrder(int id);
        void Update(int id, OrderDTO orderDTO);
        void Delete(int id);
        object GetCountOfConfirmedProducts();
        object GetCountOfConfirmedOrdersByDate();
        OrderDTO ChangeState(int id);
    }
}
