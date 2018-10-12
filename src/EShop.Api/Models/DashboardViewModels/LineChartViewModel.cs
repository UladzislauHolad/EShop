using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Models.DashboardViewModels
{
    public class LineChartViewModel
    {
        public string Name { get; set; }
        public IEnumerable<LineChartItemDTO> Series { get; set; }
    }
}
