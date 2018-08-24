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

        public ActionResult Delete(int id)
        {
            var order = _service.GetOrder(id);
            if(order != null)
            {
                _service.Delete(id);

                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}
