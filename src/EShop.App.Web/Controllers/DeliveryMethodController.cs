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
    public class DeliveryMethodController : Controller
    {
        private readonly IDeliveryMethodService _service;
        private readonly IMapper _mapper;

        public DeliveryMethodController(IDeliveryMethodService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("DeliveryMethod/api/Deliveries")]
        public ActionResult Deliveries()
        {
            var payments = _service.GetDeliveryMethods().Select(d => new { Id = d.DeliveryMethodId, Name = d.Name });

            return Json(new SelectList(payments, "Id", "Name"));
        }

        [HttpGet("api/deliveries")]
        public ActionResult GetDeliveries()
        {
            return Ok(_service.GetDeliveryMethods());
        }
    }
}
