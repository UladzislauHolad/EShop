using AutoMapper;
using EShop.App.Web.Models;
using EShop.App.Web.Models.Angular;
using EShop.App.Web.Models.Angular.Order;
using EShop.App.Web.Models.OrderViewModels;
using EShop.Services.DTO;
using EShop.Services.Infrastructure.Enums;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace EShop.App.Web.Controllers
{
    [Authorize]
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
        public ActionResult Index(string orderFilter, string searchString, DateTime from, DateTime to, string sortOrder = "OrderId_desc", int page = 1, int pageSize = 8)
        {
            var orders = _mapper.Map<IEnumerable<OrderViewModel>>(_service.GetOrders().Where(o => o.Date >= from && o.Date <= to));

            if (!string.IsNullOrEmpty(orderFilter))
            {
                orders = orders.Where(o => o.Status == orderFilter);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(o => $"{o.Customer.FirstName} {o.Customer.LastName} {o.Customer.Patronymic}".ToUpper()
                    .Contains(searchString.ToUpper()));
            }

            var name = sortOrder.Split("_")[0];
            var direction = sortOrder.Split("_")[1];

            bool desc = direction == "desc" ? true : false;
            orders = orders.AsQueryable().OrderBy(name, desc);

            SetButtonConfiguration(orders);
            var orderList = new OrderListViewModel
            {
                Orders = PaginatedList<OrderViewModel>.Create(orders, page, pageSize),
                IdSort = sortOrder == "OrderId_asc" ? "OrderId_desc" : "OrderId_asc",
                CustomerSort = sortOrder == "Customer_asc" ? "Customer_desc" : "Customer_asc",
                StatusSort = sortOrder == "Status_asc" ? "Status_desc" : "Status_asc",
                DateSort = sortOrder == "Date_asc" ? "Date_desc" : "Date_asc",
                CurrentSort = sortOrder,
                CurrentOrderFilter = orderFilter,
                CurrentSearchString = searchString,
                CurrentFrom = from,
                CurrentTo = to               
            };
            
            return View(orderList);
        }

        static T GetValue<T>(T obj)
        {
            return obj;
        }

        [HttpGet("Orders/new")]
        public ActionResult Create()
        {
            var orderToView = new OrderViewModel();
            Enum.TryParse(orderToView.Status, out StatusStates status);
            orderToView.FormConfiguration = new FormConfigurator().GetConfiguration(status);
            return View("Modify", orderToView);
        }

        [HttpPost("Orders/new")]
        public ActionResult Create(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                order.Status = "New";
                _service.Create(_mapper.Map<OrderDTO>(order));
                return RedirectToAction("Index");
            }

            Enum.TryParse(order.Status, out StatusStates status);
            order.FormConfiguration = new FormConfigurator().GetConfiguration(status);
            return View("Modify", order);
        }

        [HttpDelete("Orders/{orderId}")]
        public ActionResult Delete([FromRoute]int orderId)
        {
            try
            {
                _service.Delete(orderId);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetConfirmedProducts()
        {
            var data = _service.GetCountOfConfirmedProducts();

            return Ok(data);
        }

        [HttpGet]
        public ActionResult GetConfirmedOrdersByDate()
        {
            var data = _service.GetCountOfConfirmedOrdersByDate();

            return Ok(data);
        }

        //[HttpGet("Orders/{orderId}")]
        public ActionResult Edit([FromRoute]int orderId)
        {
            var order = _service.GetOrder(orderId);
            if(order != null)
            {
                if(order.Status == "New")
                {
                    var orderToView = _mapper.Map<OrderViewModel>(order);
                    Enum.TryParse(orderToView.Status, out StatusStates status);
                    orderToView.FormConfiguration = new FormConfigurator().GetConfiguration(status);
                    return View(orderToView);
                }
                return View("Info", (_mapper.Map<OrderViewModel>(order)));
            }

            return BadRequest();
        }

        [HttpGet("Orders/{orderId}")]
        public ActionResult Modify([FromRoute]int orderId)
        {
            var order = _service.GetOrder(orderId);
            if (order != null)
            {
                if (order.Status == "New")
                {
                    var orderToView = _mapper.Map<OrderViewModel>(order);
                    Enum.TryParse(orderToView.Status, out StatusStates status);
                    orderToView.FormConfiguration = new FormConfigurator().GetConfiguration(status);
                    return View(orderToView);
                }
                return View("Info", (_mapper.Map<OrderViewModel>(order)));
            }

            return BadRequest();
        }

        [HttpPost("Orders/{orderId}")]
        public ActionResult Modify([FromRoute]int orderId, OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                order.Status = StatusStates.New.ToString();
                _service.Update(_mapper.Map<OrderDTO>(order));
                return RedirectToAction("Modify", new { orderId = orderId });
            }
            return View(order);
        }

        [HttpPatch("Orders/api/{orderId}")]
        public ActionResult ChangeState(int orderId)
        {
            try
            {
                _service.ChangeState(orderId);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }        

        private void SetButtonConfiguration(IEnumerable<OrderViewModel> orders)
        {
            ButtonConfigurator configurator = new ButtonConfigurator();

            foreach (var order in orders)
            {
                order.ButtonConfiguration = configurator.GetConfiguration(order.Command);
            }
        }

        [HttpGet("api/orders")]
        [AllowAnonymous]
        public ActionResult GetOrders()//ActionREsult
        {
            return Ok(_mapper.Map<IEnumerable<OrderAngularViewModel>>(_service.GetOrders()));
        }

        [HttpGet("api/orders/{id}")]
        [AllowAnonymous]
        public ActionResult GetOrder([FromRoute]int id)
        {
            return Ok(_mapper.Map<OrderInfoAngularViewModel>(_service.GetOrder(id)));
        }

        [HttpPost("api/orders")]
        [AllowAnonymous]
        public ActionResult CreateOrder([FromBody]ModifyOrderAngularViewModel order)
        {
            if (ModelState.IsValid)
            {
                order.Status = "New";
                var result = _service.Create(_mapper.Map<OrderDTO>(order));

                return StatusCode(StatusCodes.Status201Created, _mapper.Map<ModifyOrderAngularViewModel>(result));
            }

            return BadRequest();//422 entity invalid state
        }

        [HttpPatch("api/orders/{id}")]
        [AllowAnonymous]
        public ActionResult UpdateOrder([FromRoute]int id, [FromBody]ModifyOrderAngularViewModel order)
        {
            if(ModelState.IsValid)
            {
                _service.Update(_mapper.Map<OrderDTO>(order));
                return NoContent();
            }

            return BadRequest();
        }

        [HttpPut("api/orders/{orderId}")]
        public ActionResult ChangeOrderState([FromRoute]int orderId)
        {
            try
            {
                var result = _service.ChangeState(orderId);
                return Ok(_mapper.Map<OrderAngularViewModel>(result));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/orders/{orderId}")]
        public ActionResult DeleteOrder([FromRoute]int orderId)
        {
            try
            {
                _service.Delete(orderId);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
