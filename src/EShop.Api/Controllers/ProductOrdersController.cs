using AutoMapper;
using EShop.Api.Infrastructure;
using EShop.Api.Models.OrdersViewModels;
using EShop.Api.Models.ProductOrdersViewModels;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Controllers
{
    [Authorize]
    [Route("api/orders")]
    [Produces("application/json")]
    public class ProductOrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductOrderService _productOrderService;
        private readonly IMapper _mapper;

        public ProductOrdersController(IOrderService orderService, IProductOrderService productOrderService, IMapper mapper)
        {
            _orderService = orderService;
            _productOrderService = productOrderService;
            _mapper = mapper;
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
        [HttpGet("{orderId}/products")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<ProductOrderViewModel>> GetProductOrders([FromRoute]int orderId)
        {
            var order = _orderService.GetOrder(orderId);
            if (order != null)
            {
                var result = _mapper.Map<IEnumerable<ProductOrderViewModel>>(order.ProductOrders);

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
        /// <response code="201">New instance of ProductOrderViewModel in ProductOrders collection of OrderViewModel was created</response>
        /// <response code="400">OrderId or ProductOrderCreateViewModel is not valid</response>
        [HttpPost("{orderId}/products")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult CreateProductOrder([FromRoute]int orderId, [FromBody]CreateProductOrderViewModel productOrderCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ModelState.ToErrorsStringArray());
            }

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
        [HttpDelete("{orderId}/products/{productOrderId}")]
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
        /// <response code="422">Wrong model</response>
        [HttpPatch("{orderId}/products/{productOrderId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public ActionResult UpdateProductOrder([FromRoute]int orderId, [FromRoute]int productOrderId, [FromBody]NewOrderCount OrderCount)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ModelState.ToErrorsStringArray());
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
                catch (InvalidOperationException ex)
                {
                    return BadRequest(new { ex.Message });
                }
            }

            return BadRequest();
        }
    }
}
