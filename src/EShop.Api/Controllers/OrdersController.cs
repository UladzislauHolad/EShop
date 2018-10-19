using AutoMapper;
using EShop.Api.Infrastructure;
using EShop.Api.Models.OrdersViewModels;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNet.OData;
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
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of orders
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Get api/orders
        /// 
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Returns list of orders</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [EnableQuery]
        public IQueryable<OrderTableViewModel> Get()
        {
            return _mapper.Map<IEnumerable<OrderTableViewModel>>(_service.GetOrders()).AsQueryable();
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        ///     
        ///     Get api/orders/1
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Returns order</response>
        /// <response code="204">Returns no content</response>
        [HttpGet("api/orders/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public OrderViewModel GetOrder([FromRoute]int id)
        {
            var order = _mapper.Map<OrderViewModel>(_service.GetOrder(id));
            return order;
        }

        /// <summary>
        /// Create new instance of Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <response code="201">Order was created</response>
        /// <response code="422">Invalid model</response>
        [HttpPost("api/orders")]
        [ProducesResponseType(201)]
        [ProducesResponseType(422)]
        public ActionResult CreateOrder([FromBody]ModifyOrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                order.Status = "New";
                var result = _service.Create(_mapper.Map<OrderDTO>(order));

                return StatusCode(StatusCodes.Status201Created, _mapper.Map<ModifyOrderViewModel>(result));
            }

            return StatusCode(StatusCodes.Status422UnprocessableEntity, ModelState.ToErrorsStringArray());
        }

        /// <summary>
        /// Update order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <response code="204">Order was updated</response>
        /// <response code="422">Invalid model</response>
        [HttpPatch("api/orders/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(422)]
        public ActionResult UpdateOrder([FromRoute]int id, [FromBody]ModifyOrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                _service.Update(id, _mapper.Map<OrderDTO>(order));
                return NoContent();
            }

            return StatusCode(StatusCodes.Status422UnprocessableEntity);
        }

        /// <summary>
        /// Change order state
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Order state is changed</response>
        /// <response code="422">Can not change state of this order</response>
        [HttpPut("api/orders/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public ActionResult ChangeOrderState([FromRoute]int id)
        {
            try
            {
                var result = _service.ChangeState(id);
                return Ok(_mapper.Map<OrderTableViewModel>(result));
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
        }

        /// <summary>
        /// Delete order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Order was deleted</response>
        /// <response code="422">Can not delete this order</response>
        [HttpDelete("api/orders/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public ActionResult DeleteOrder([FromRoute]int id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
        }
    }
}
