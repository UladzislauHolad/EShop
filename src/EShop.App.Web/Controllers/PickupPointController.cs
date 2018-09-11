using AutoMapper;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Controllers
{
    public class PickupPointController : Controller
    {
        IPickupPointService _service;
        readonly IMapper _mapper;

        public PickupPointController(IPickupPointService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public JsonResult PickupPoints()
        {
            var points = _service.GetPickupPoints().Select(p => new { Id = p.PickupPointId, Name = p.Name });

            return Json(new SelectList(points, "Id", "Name"));
        }
    }
}
