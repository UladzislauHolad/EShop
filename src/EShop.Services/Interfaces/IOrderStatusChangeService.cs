using EShop.Services.DTO;
using EShop.Services.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Interfaces
{
    public interface IOrderStatusChangeService
    {
        IEnumerable<OrderStatusChartInfoDTO> GetInfoByStatus(StatusStates status);
        IEnumerable<LineChartItemDTO> GetOrdersByState(StatusStates status);
    }
}
