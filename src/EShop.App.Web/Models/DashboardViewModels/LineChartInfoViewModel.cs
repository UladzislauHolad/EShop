using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.DashboardViewModels
{
    public class LineChartInfoViewModel
    {
        public IEnumerable<OrderStatusChartInfoDTO> New { get; set; }
        public IEnumerable<OrderStatusChartInfoDTO> Confirmed { get; set; }
        public IEnumerable<OrderStatusChartInfoDTO> Paid { get; set; }
        public IEnumerable<OrderStatusChartInfoDTO> Completed { get; set; }
    }
}
