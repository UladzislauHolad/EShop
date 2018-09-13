using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.App.Web.Models.DashboardViewModels;
using EShop.Services.Infrastructure.Enums;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.App.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IOrderStatusChangeService _service;

        public DashboardController(IOrderStatusChangeService service)
        {
            _service = service;
        }

        [HttpGet("Dashboard")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Dashboard/Line")]
        public ActionResult GetLineCharInfo()
        {
            var data = new LineChartInfoViewModel
            {
                New = _service.GetInfoByStatus(StatusStates.New),
                Confirmed = _service.GetInfoByStatus(StatusStates.Confirmed),
                Paid = _service.GetInfoByStatus(StatusStates.Paid),
                Completed = _service.GetInfoByStatus(StatusStates.Completed)
            };

            return Json(data);
        }
    }
}