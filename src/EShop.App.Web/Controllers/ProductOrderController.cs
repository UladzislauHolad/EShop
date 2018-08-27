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
            var order = _mapper.Map<OrderViewModel>(_orderService.GetOrder(id));
            if (order != null)
            {
                ViewBag.OrderId = order.OrderId;
                return View(order.ProductOrders); //добавить модельдля отображения информации о заказе
            }
            return NotFound();
        }

        [HttpGet("Orders/{id}/Products")]
        public ActionResult Create([FromRoute]int id)
        {
            var order = _mapper.Map<OrderViewModel>(_orderService.GetOrder(id));
            if (order != null)
            {
                return View(new ProductOrderViewModel { OrderId = order.OrderId });
            }
            return NotFound();
        }

        [HttpPost("Orders/{id}/Products")]
        public ActionResult Create(ProductOrderViewModel productOrder)
        {
            var order = _mapper.Map<OrderViewModel>(_orderService.GetOrder(productOrder.OrderId));
            if (order != null)
            {
                var product = new ProductViewModel
                {
                    ProductId = productOrder.ProductId,
                    Name = productOrder.Name,
                    Price = productOrder.Price,
                    Description = productOrder.Description,
                    Count = productOrder.Count
                };
                productOrder.Product = product;
                _productOrderService.Create(_mapper.Map<ProductOrderDTO>(productOrder));

                return RedirectToAction("Index", new { id = order.OrderId });
            }
            return NotFound();
        }

        [HttpDelete("Orders/{orderId}/Products/{productOrderId}")]
        public ActionResult Delete([FromRoute]int orderId, [FromRoute]int productOrderId)
        {
            var productOrder = _productOrderService.GetProductOrder(productOrderId);
            if(productOrder != null)
            {
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
                productOrder.OrderCount = OrderCount.OrderCount;
                _productOrderService.Update(_mapper.Map<ProductOrderDTO>(productOrder));

                return Json(new { success = true });
            }

            return BadRequest();
        }
    }
}
