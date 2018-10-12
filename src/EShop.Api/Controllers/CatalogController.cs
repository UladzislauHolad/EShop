using AutoMapper;
using EShop.Api.Models;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Controllers
{
    [Authorize]
    [Route("api/catalog")]
    [Produces("application/json")]
    public class CatalogController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _service;

        public CatalogController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of CategoryNestedNodeViewModels.
        /// </summary>
        /// <returns>A list of CategoryNestedNodeViewModels</returns>
        /// <response code="200">Returns the list of CategoryNestedNodeViewModels</response>
        [HttpGet("category-nodes")]
        [ProducesResponseType(200)]
        public IEnumerable<CategoryNestedNodeViewModel> GetCategoryNestedNodes()
        {
            var result = _mapper.Map<IEnumerable<CategoryNestedNodeViewModel>>(_service.GetCategoryNestedNodes());

            return result;
        }
    }
}
