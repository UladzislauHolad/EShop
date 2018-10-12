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
    [Route("api/payments")]
    [Produces("application/json")]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IPaymentMethodService _service;
        private readonly IMapper _mapper;

        public PaymentMethodsController(IPaymentMethodService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of PaymentMethods
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Get api/payments
        ///     
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Returns list of PaymentMethods</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public IEnumerable<PaymentMethodDTO> GetPayments()
        {
            return _service.GetPaymentMethods();
        }
    }
}
