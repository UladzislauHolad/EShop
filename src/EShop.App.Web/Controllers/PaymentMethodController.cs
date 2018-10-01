using AutoMapper;
using EShop.App.Web.Models.Angular.PaymentMethod;
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
    public class PaymentMethodController : Controller
    {
        private readonly IPaymentMethodService _service;
        private readonly IMapper _mapper;

        public PaymentMethodController(IPaymentMethodService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("PaymentMethod/api/Payments")]
        public ActionResult Payments()
        {
            var payments = _service.GetPaymentMethods().Select(p => new { Id = p.PaymentMethodId, Name = p.Name });

            return Ok(new SelectList(payments, "Id", "Name"));
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
        [HttpGet("api/payments")]
        [ProducesResponseType(200)]
        public IEnumerable<PaymentMethodAngularViewModel> GetPayments()
        {
            return _mapper.Map<IEnumerable<PaymentMethodAngularViewModel>>(_service.GetPaymentMethods());
        }
    }
}
