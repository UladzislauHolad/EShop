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
    [Route("api/deliveries")]
    [Produces("application/json")]
    public class DeliveryMethodsController : ControllerBase
    {
        private readonly IDeliveryMethodService _service;
        private readonly IMapper _mapper;

        public DeliveryMethodsController(IDeliveryMethodService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
        [HttpGet]
        [ProducesResponseType(200)]
        public IEnumerable<DeliveryMethodDTO> GetDeliveries()
        {
            return _service.GetDeliveryMethods();
        }
    }
}
