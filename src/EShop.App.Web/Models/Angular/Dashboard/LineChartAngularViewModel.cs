using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.Angular.Dashboard
{
    public class LineChartAngularViewModel
    {
        public string Name { get; set; }
        public IEnumerable<LineChartItemDTO> Series { get; set; }
    }
}
