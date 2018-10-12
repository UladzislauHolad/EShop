using AutoMapper;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Controllers
{
    [Authorize]
    [Route("api/pickups")]
    [Produces("application/json")]
    public class PickupPointsController : ControllerBase
    {
        IPickupPointService _service;
        readonly IMapper _mapper;

        public PickupPointsController(IPickupPointService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
        [HttpGet]
        [ProducesResponseType(200)]
        public IEnumerable<PickupPointDTO> GetPickups()
        {
            return _service.GetPickupPoints();
        }
    }
}
