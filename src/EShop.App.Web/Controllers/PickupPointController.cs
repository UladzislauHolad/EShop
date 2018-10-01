using AutoMapper;
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
    public class PickupPointController : Controller
    {
        IPickupPointService _service;
        readonly IMapper _mapper;

        public PickupPointController(IPickupPointService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public ActionResult PickupPoints()
        {
            var points = _service.GetPickupPoints().Select(p => new { Id = p.PickupPointId, Name = p.Name });

            return Ok(new SelectList(points, "Id", "Name"));
        }

        /// <summary>
        /// Get list of PickupPoints
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Get api/pickups
        ///     
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Returns list of PickupPoints</response>
        [HttpGet("api/pickups")]
        [ProducesResponseType(200)]
        public IEnumerable<PickupPointDTO> GetPickups()
        {
            return _service.GetPickupPoints();
        }
    }
}
