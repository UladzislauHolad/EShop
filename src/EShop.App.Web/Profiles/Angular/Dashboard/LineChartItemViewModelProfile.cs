using AutoMapper;
using EShop.App.Web.Models.Angular.Dashboard;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles.Angular.Dashboard
{
    public class LineChartItemViewModelProfile : Profile
    {
        public LineChartItemViewModelProfile()
        {
            CreateMap<LineChartItemViewModel, LineChartItemDTO>();
            CreateMap<LineChartItemDTO, LineChartItemViewModel>();
        }
    }
}
