using AutoMapper;
using EShop.Api.Models.CategoriesViewModels;
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
    [Route("api/categories")]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        /// <summary>
        /// Get category info for pie chart
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet("pie-chart")]
        [ProducesResponseType(200)]
        public IEnumerable<CategoryPieChartInfoDTO> CategoryWithCountOfProducts()
        {
            return _service.GetCategoryNameWithCountOfProducts();
        }

        /// <summary>
        /// Get list of categories
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns list of categories</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public IEnumerable<CategoryViewModel> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(_service.GetCategories());
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Return category</response>
        /// <response code="204">Return no content</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public CategoryViewModel GetCategory([FromRoute]int id)
        {
            return _mapper.Map<CategoryViewModel>(_service.GetCategory(id));
        }

        /// <summary>
        /// Create new instance of category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <response code="201">Category was created</response>
        /// <response code="422">Invalid model</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(422)]
        public ActionResult CreateCategory([FromBody]CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                _service.Create(_mapper.Map<CategoryDTO>(category));
                return StatusCode(StatusCodes.Status201Created);
            }

            return StatusCode(StatusCodes.Status422UnprocessableEntity);
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <response code="204">Category was updated</response>
        /// <response code="422">Model is invalid</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(422)]
        public ActionResult UpdateCategory([FromRoute]int id, [FromBody]CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                _service.Update(_mapper.Map<CategoryDTO>(category));
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return StatusCode(StatusCodes.Status422UnprocessableEntity);
        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Category was deleted</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public ActionResult DeleteCategory([FromRoute]int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}
