using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _service;
        private readonly IMapper _mapper;

        public OrderController(IOrderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ViewResult Index()
        {
            var orders = _mapper.Map<IEnumerable<OrderViewModel>>(_service.GetOrders()
                .OrderByDescending(o => o.OrderId));

            return View(orders);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new OrderViewModel());
        }

        [HttpPost]
        public ActionResult Create(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                _service.Create(_mapper.Map<OrderDTO>(order));
                return RedirectToAction("Index");
            }

            return View(order);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var order = _service.GetOrder(id);
            if(order != null)
            {
                if (order.IsConfirmed)
                {
                    return BadRequest();
                }
                _service.Delete(id);

                return Json(new { success = true });
            }

            return NotFound();
        }

        [HttpPatch]
        //Orders/1
        public ActionResult Confirm(int id)
        {
            var order = _service.GetOrder(id);
            if(order != null)
            {
                if(order.IsConfirmed)
                {
                    return BadRequest();
                }
                if(_service.IsConfirmAvailable(id))
                {
                    _service.Confirm(id);

                    return Ok();
                }
                else
                {
                    return BadRequest(new { message = "This order have not available products" });
                }
            }

            return NotFound();
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
    }
}
