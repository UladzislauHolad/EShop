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
            var orders = _mapper.Map<IEnumerable<OrderViewModel>>(_service.GetOrders());

            return View(orders);
        }

        [HttpGet]
        public ActionResult Create()
        {
            _service.Create(new OrderDTO());
            return RedirectToAction("Index");
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

                return Json(new { succes = true });
            }

            return NotFound();
        }

        [HttpPatch]
        public ActionResult Confirm(int id)
        {
            var order = _service.GetOrder(id);
            if(order != null)
            {
                if(order.IsConfirmed)
                {
                    return BadRequest();
                }
                order.IsConfirmed = true;
                order.Date = DateTime.Now;
                _service.Update(order);

                return Json(new { succes = true });
            }

            return NotFound();
        }
    }
}
