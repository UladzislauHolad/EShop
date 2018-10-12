using AutoMapper;
using EShop.Api.Models.DashboardViewModels;
using EShop.Services.Infrastructure.Enums;
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
    [Route("api/dashboard")]
    [Produces("application/json")]
    public class DashboardController : ControllerBase
    {
        private readonly IOrderStatusChangeService _service;
        private readonly IMapper _mapper;

        public DashboardController(IOrderStatusChangeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get order data for line chart
        /// </summary>
        /// <param name="states"></param>
        /// <returns></returns>
        /// <response code="200">Returns order data for line chart</response>
        /// <response code="422">Wrong parameters</response>
        [HttpGet("line/orders")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [AllowAnonymous]
        public ActionResult<IEnumerable<LineChartViewModel>> GetOrdersByStateInfo(string[] states)
        {
            List<LineChartViewModel> responseList = new List<LineChartViewModel>();

            try
            {
                foreach (var s in states)
                {
                    StatusStates state;
                    Enum.TryParse(s, out state);
                    var part = new LineChartViewModel
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
