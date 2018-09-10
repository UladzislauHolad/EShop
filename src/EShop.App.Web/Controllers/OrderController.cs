using AutoMapper;
using EShop.App.Web.Models;
using EShop.App.Web.Models.OrderViewModels;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _service;
        private readonly IMapper _mapper;

        public OrderController(IOrderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("Orders")]
        public ViewResult Index()
        {
            var orders = _mapper.Map<IEnumerable<OrderViewModel>>(_service.GetOrders()
                .OrderByDescending(o => o.OrderId));
            SetButtonConfiguration(orders);
            
            return View(orders);
        }

        [HttpGet("Orders/new")]
        public ActionResult Create()
        {
            return View(new OrderViewModel());
        }

        [HttpPost("Orders/new")]
        public ActionResult Create(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                _service.Create(_mapper.Map<OrderDTO>(order));
                return RedirectToAction("Index");
            }

            return View(order);
        }

        [HttpDelete("Orders/{orderId}")]
        public ActionResult Delete([FromRoute]int orderId)
        {
            try
            {
                _service.Delete(orderId);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public JsonResult GetConfirmedProducts()
        {
            var data = _service.GetCountOfConfirmedProducts();

            return Json(data);
        }

        [HttpGet]
        public JsonResult GetConfirmedOrdersByDate()
        {
            var data = _service.GetCountOfConfirmedOrdersByDate();

            return Json(data);
        }

        [HttpGet("Orders/{orderId}")]
        public ActionResult Edit([FromRoute]int orderId)
        {
            var order = _service.GetOrder(orderId);
            if(order != null)
            {
                if(order.Status == "New")
                    return View(_mapper.Map<OrderViewModel>(order));
                return View("Info", (_mapper.Map<OrderViewModel>(order)));
            }

            return BadRequest();
        }

        [HttpPatch("Orders/{orderId}")]
        public ActionResult Edit([FromRoute]int orderId , OrderViewModel order)
        {
            if(ModelState.IsValid)
            {
                var existOrder = _service.GetOrder(orderId);
                if (existOrder != null && existOrder.Status == "New")
                {
                    existOrder.Customer = _mapper.Map<CustomerDTO>(order.Customer);
                    existOrder.PaymentMethod = null;
                    existOrder.PaymentMethodId = order.PaymentMethodId;
                    existOrder.DeliveryMethod = null;
                    existOrder.DeliveryMethodId = order.DeliveryMethodId;
                    _service.Update(_mapper.Map<OrderDTO>(existOrder));
                    return Ok();
                }
            }
            
            return BadRequest();
        }

        [HttpPatch("Orders/api/{orderId}")]
        public ActionResult ChangeState(int orderId)
        {
            try
            {
                _service.ChangeState(orderId);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void SetButtonConfiguration(IEnumerable<OrderViewModel> orders)
        {
            ButtonConfigurator configurator = new ButtonConfigurator();

            foreach (var order in orders)
            {
                order.ButtonConfiguration = configurator.GetConfiguration(order.Command);
            }
        }
    }
}
