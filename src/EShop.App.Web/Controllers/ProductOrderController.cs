using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Controllers
{
    public class ProductOrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductOrderService _productOrderService;
        private readonly IMapper _mapper;

        public ProductOrderController(IOrderService orderService, IProductOrderService productOrderService, IMapper mapper)
        {
            _orderService = orderService;
            _productOrderService = productOrderService;
            _mapper = mapper;
        }

        [HttpGet("Orders/{id}")]
        public ActionResult Index([FromRoute]int id)
        {
            var order = _orderService.GetOrder(id);
            if (order != null)
            {
                return View(_mapper.Map<ProductOrderListViewModel>(order));
            }
            return NotFound();
        }

        [HttpGet("Orders/{id}/Products")]
        public ActionResult Create([FromRoute]int id)
        {
            var order = _mapper.Map<OrderViewModel>(_orderService.GetOrder(id));
            if (order != null)
            {
                if(order.IsConfirmed)
                {
                    return BadRequest();
                }

                return View(new ProductOrderCreateViewModel { OrderId = order.OrderId });
            }
            return NotFound();
        }

        [HttpPost("Orders/{id}/Products")]
        public ActionResult Create(ProductOrderCreateViewModel productOrderCreateModel)
        {
            var order = _mapper.Map<OrderViewModel>(_orderService.GetOrder(productOrderCreateModel.OrderId));
            if (order != null)
            {
                if (order.IsConfirmed)
                {
                    return BadRequest();
                }
                _productOrderService.Create(_mapper.Map<ProductOrderDTO>(productOrderCreateModel));

                return RedirectToAction("Index", new { id = order.OrderId });
            }
            return NotFound();
        }

        [HttpDelete("Orders/{orderId}/Products/{productOrderId}")]
        public ActionResult Delete([FromRoute]int orderId, [FromRoute]int productOrderId)
        {
            var order = _orderService.GetOrder(orderId);
            var productOrder = _productOrderService.GetProductOrder(productOrderId);
            if(productOrder != null && order != null)
            {
                if (order.IsConfirmed)
                {
                    return BadRequest();
                }
                _productOrderService.Delete(productOrderId);

                return Json(new { success = true });
            }
            return NotFound();
        }

        [HttpPatch("Orders/{orderId}/Products/{productOrderId}")]
        public ActionResult Edit([FromRoute]int OrderId, [FromRoute]int productOrderId, [FromBody]NewOrderCount OrderCount)
        {
            var order = _orderService.GetOrder(OrderId);
            var productOrder = _productOrderService.GetProductOrder(productOrderId);
            if (order != null && productOrder != null)
            {
                if (order.IsConfirmed)
                {
                    return BadRequest();
                }
                productOrder.OrderCount = OrderCount.OrderCount;
                _productOrderService.Update(_mapper.Map<ProductOrderDTO>(productOrder));

                return Json(new { success = true });
            }

            return BadRequest();
        }
    }
}
