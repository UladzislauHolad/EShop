using AutoMapper;
using EShop.App.Web.Models;
using EShop.App.Web.Models.Angular.ProductOrder;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Controllers
{
    [Authorize]
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

        [HttpGet]
        public ActionResult Index([FromRoute]int id)
        {
            var order = _orderService.GetOrder(id);
            if (order != null)
            {
                return View(_mapper.Map<ProductOrderListViewModel>(order));
            }
            return NotFound();
        }


        [HttpGet("Orders/test/{orderId}/Products")]
        public ActionResult Products([FromRoute]int orderId)
        {
            var order = _orderService.GetOrder(orderId);
            if (order != null)
            {
                var result = _mapper.Map<IEnumerable<ProductOrderViewModel>>(order.ProductOrders);
                foreach (var item in result)
                {
                    item.Product = null;
                }
                return Json(result);
            }
            return Json(null);////////////////////////////////////////////////////////////////////////////////////////////?
        }

        [HttpGet("Orders/{orderId}/Products")]
        public ActionResult Create([FromRoute]int orderId)
        {
            var order = _mapper.Map<OrderViewModel>(_orderService.GetOrder(orderId));
            if (order != null)
            {
                if(order.Status != "New")
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
                if (order.Status != "New")
                {
                    return BadRequest();
                }
                _productOrderService.Create(_mapper.Map<ProductOrderDTO>(productOrderCreateModel));

                return RedirectToAction("Modify", "Order", new { orderId = order.OrderId });
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
                if (order.Status != "New")
                {
                    return BadRequest();
                }
                _productOrderService.Delete(productOrderId);

                return Ok();
            }
            return NotFound();
        }

        [HttpPatch("Orders/{orderId}/Products/{productOrderId}")]
        public ActionResult Edit([FromRoute]int orderId, [FromRoute]int productOrderId, [FromBody]NewOrderCount OrderCount)
        {
            var order = _orderService.GetOrder(orderId);
            var productOrder = _productOrderService.GetProductOrder(productOrderId);
            if (order != null && productOrder != null)
            {
                if (order.Status != "New")
                {
                    return BadRequest();
                }
                productOrder.OrderCount = OrderCount.OrderCount;
                _productOrderService.Update(_mapper.Map<ProductOrderDTO>(productOrder));

                return Json(new { success = true });
            }

            return BadRequest();
        }

        /// <summary>
        /// Get list of ProductOrderAngularViewModel by orderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Get api/orders/1/products
        /// 
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Returns the list of ProductOrderAngularViewModel</response>
        /// <response code="400">OrderId is not valid</response>
        [HttpGet("api/orders/{orderId}/products")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<ProductOrderAngularViewModel>> GetProductOrders([FromRoute]int orderId)
        {
            var order = _orderService.GetOrder(orderId);
            if (order != null)
            {
                var result = _mapper.Map<IEnumerable<ProductOrderAngularViewModel>>(order.ProductOrders);

                return Ok(result);
            }

            return BadRequest();
        }

        /// <summary>
        /// Create new instance of ProductOrderViewModel in ProductOrders collection of OrderViewModel
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Post api/orders/1/products 
        ///     {
        ///         "orderId": 1,
        ///         "orderCount": 1,
        ///         "productId": 1
        ///     }
        /// </remarks>
        /// <param name="orderId"></param>
        /// <param name="productOrderCreateModel"></param>
        /// <returns></returns>
        /// <response code="201">new instance of ProductOrderViewModel in ProductOrders collection of OrderViewModel was created</response>
        /// <response code="400">OrderId or ProductOrderCreateViewModel is not valid</response>
        [HttpPost("api/orders/{orderId}/products")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult CreateProductOrder([FromRoute]int orderId, [FromBody]ProductOrderCreateViewModel productOrderCreateModel)
        {
            var order = _mapper.Map<OrderViewModel>(_orderService.GetOrder(orderId));
            if (order != null && ModelState.IsValid && orderId == productOrderCreateModel.OrderId)
            {
                if (order.Status != "New")
                {
                    return BadRequest();
                }
                _productOrderService.Create(_mapper.Map<ProductOrderDTO>(productOrderCreateModel));

                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest();
        }

        /// <summary>
        /// Delete ProductOrder
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        ///     Delete api/order/1/products/1
        /// </remarks>
        /// <param name="orderId"></param>
        /// <param name="productOrderId"></param>
        /// <returns></returns>
        /// <response code="200">ProductOrder was deleted</response>
        /// <response code="400">ProductOrder was not found</response>
        [HttpDelete("api/orders/{orderId}/products/{productOrderId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteProductOrder([FromRoute]int orderId, [FromRoute]int productOrderId)
        {
            var order = _orderService.GetOrder(orderId);
            var productOrder = _productOrderService.GetProductOrder(productOrderId);
            if (productOrder != null && order != null)
            {
                if (order.Status != "New")
                {
                    return BadRequest();
                }
                _productOrderService.Delete(productOrderId);

                return Ok();
            }
            return NotFound();
        }

        /// <summary>
        /// Update ProductOrder
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Patch api/orders/1/products/1
        ///     {
        ///         "orderCount": 1,
        ///         "maxCount": 2
        ///     }
        /// </remarks>
        /// <param name="orderId"></param>
        /// <param name="productOrderId"></param>
        /// <param name="OrderCount"></param>
        /// <returns></returns>
        /// <response code="204">ProductOrder was updated</response>
        /// <response code="400">Wrong parameters</response>
        [HttpPatch("api/orders/{orderId}/products/{productOrderId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public ActionResult UpdateProductOrder([FromRoute]int orderId, [FromRoute]int productOrderId, [FromBody]NewOrderCount OrderCount)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Wrong data" });
            }

            var order = _orderService.GetOrder(orderId);
            var productOrder = _productOrderService.GetProductOrder(productOrderId);
            if (order != null && productOrder != null)
            {
                if (order.Status != "New")
                {
                    return BadRequest();
                }
                productOrder.OrderCount = OrderCount.OrderCount;
                try
                {
                    _productOrderService.Update(_mapper.Map<ProductOrderDTO>(productOrder));
                    return Ok();
                }
                catch(InvalidOperationException ex)
                {
                    return BadRequest(new { ex.Message });
                }
            }

            return BadRequest();
        }
    }
}
