﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShop.App.Web.Models.Angular.Dashboard;
using EShop.App.Web.Models.DashboardViewModels;
using EShop.Services.Infrastructure.Enums;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.App.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IOrderStatusChangeService _service;
        private readonly IMapper _mapper;

        public DashboardController(IOrderStatusChangeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("Dashboard")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Dashboard/Line")]
        [AllowAnonymous]
        public ActionResult GetLineCharInfo()
        {
            var data = new LineChartInfoViewModel
            {
                New = _service.GetInfoByStatus(StatusStates.New),
                Confirmed = _service.GetInfoByStatus(StatusStates.Confirmed),
                Paid = _service.GetInfoByStatus(StatusStates.Paid),
                Completed = _service.GetInfoByStatus(StatusStates.Completed)
            };

            return Ok(data);
        }

        /// <summary>
        /// Get order data for line chart
        /// </summary>
        /// <param name="states"></param>
        /// <returns></returns>
        /// <response code="200">Returns order data for line chart</response>
        /// <response code="422">Wrong parameters</response>
        [HttpGet("api/dashboard/line/orders")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [AllowAnonymous]
        public ActionResult<IEnumerable<LineChartAngularViewModel>> GetOrdersByStateInfo(string[] states)
        {
            List<LineChartAngularViewModel> responseList = new List<LineChartAngularViewModel>();

            try
            {
                foreach (var s in states)
                {
                    StatusStates state;
                    Enum.TryParse(s, out state);
                    var part = new LineChartAngularViewModel
                    {
                        Name = state.ToString(),
                        Series = _service.GetOrdersByState(state)
                    };
                    responseList.Add(part);
                }
                return Ok(responseList);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
        }
    }
}