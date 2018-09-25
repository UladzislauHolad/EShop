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

        [HttpGet("api/payments")]
        public ActionResult GetPayments()
        {
            return Ok(_service.GetPaymentMethods());
        }
    }
}
