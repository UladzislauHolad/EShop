using AutoMapper;
using EShop.App.Web.Models.Angular.DeliveryMethod;
using EShop.Services.DTO;
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

        /// <summary>
        /// Get list of DeliveryMethods
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Get api/deliveries
        ///     
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Returns list of DeliveryMethods</response>
        [HttpGet("api/deliveries")]
        [ProducesResponseType(200)]
        public IEnumerable<DeliveryMethodAngularViewModel> GetDeliveries()
        {
            return _mapper.Map<IEnumerable<DeliveryMethodAngularViewModel>>(_service.GetDeliveryMethods());
        }
    }
}
