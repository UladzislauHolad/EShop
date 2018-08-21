using AutoMapper;
using EShop.App.Web.Models;
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
        public ViewResult Create()
        {
            return View(new OrderViewModel());
        }

        [HttpGet]
        public ViewResult Create(OrderViewModel order)
        {
            return View(new OrderViewModel());
        }
    }
}
