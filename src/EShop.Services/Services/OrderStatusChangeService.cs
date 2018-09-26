using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Infrastructure.Enums;
using EShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Services.Services
{
    public class OrderStatusChangeService : IOrderStatusChangeService
    {
        private readonly IRepository<OrderStatusChange> _repository;

        public OrderStatusChangeService(IRepository<OrderStatusChange> repository)
        {
            _repository = repository;
        }
                
        public IEnumerable<OrderStatusChartInfoDTO> GetInfoByStatus(StatusStates status)
        {
            return _repository.GetAll()
                .Where(os => os.Status == status.ToString())
                .GroupBy(os => os.Date.Date)
                .Select(g => new OrderStatusChartInfoDTO { Date = g.Key, Count = g.Count() });
        }

        public IEnumerable<LineChartItemDTO> GetOrdersByState(StatusStates status)
        {
            return _repository.GetAll()
                .Where(os => os.Status == status.ToString())
                .GroupBy(os => os.Date.Date)
                .Select(g => new LineChartItemDTO { Name = g.Key, Value = g.Count() });
        }
    }
}
