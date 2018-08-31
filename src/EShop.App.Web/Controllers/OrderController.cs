﻿using AutoMapper;
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

        [HttpGet("Orders")]
        public ViewResult Index()
        {
            var orders = _mapper.Map<IEnumerable<OrderViewModel>>(_service.GetOrders()
                .OrderByDescending(o => o.OrderId));

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
            var order = _service.GetOrder(orderId);
            if(order != null)
            {
                if (order.IsConfirmed)
                {
                    return BadRequest();
                }
                _service.Delete(orderId);

                return Ok();
            }

            return NotFound();
        }

        [HttpPatch("Orders/api/{orderId}")]
        public ActionResult Confirm(int orderId)
        {
            var order = _service.GetOrder(orderId);
            if(order != null)
            {
                if(order.IsConfirmed)
                {
                    return BadRequest();
                }
                if(_service.IsConfirmAvailable(orderId))
                {
                    _service.Confirm(orderId);

                    return Ok();
                }
                else
                {
                    return BadRequest();
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

        [HttpGet("Orders/{orderId}")]
        public ActionResult Edit([FromRoute]int orderId)
        {
            var order = _service.GetOrder(orderId);
            if(order != null)
            {
                if(!order.IsConfirmed)
                    return View(_mapper.Map<OrderViewModel>(order));
                return View("Info", (_mapper.Map<OrderViewModel>(order)));
            }

            return BadRequest();
        }

        [HttpPatch("Orders/{orderId}")]
        public ActionResult Edit([FromRoute]int orderId , OrderViewModel order)
        {
            var existOrder = _service.GetOrder(orderId);
            if (existOrder != null && !existOrder.IsConfirmed)
            {
                existOrder.Customer = _mapper.Map<CustomerDTO>(order.Customer);
                _service.Update(_mapper.Map<OrderDTO>(existOrder));
                return Ok();
            }
            
            return BadRequest();
        }
    }
}
