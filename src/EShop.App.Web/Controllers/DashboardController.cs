using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EShop.App.Web.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet("Dashboard")]
        public IActionResult Index()
        {
            return View();
        }
    }
}