using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetOrders();
        void Create(OrderDTO orderDTO);
        OrderDTO GetOrder(int id);
        void Update(OrderDTO orderDTO);
        void Delete(int id);
        void Confirm(int id);
        bool IsConfirmAvailable(int id);
        object GetCountOfConfirmedProducts();
    }
}
