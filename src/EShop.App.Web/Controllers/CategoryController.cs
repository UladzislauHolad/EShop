using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.App.Web.Controllers
{
    //[Authorize]
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _service;
        public int PageSize = 8;

        public CategoryController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: Category
        [HttpGet("Categories")]
        [HttpGet("Categories/Pages/{page}")]
        public ViewResult Index(int page = 1)
        {
            var categories = _mapper.Map<IEnumerable<CategoryDTO> ,IEnumerable<CategoryViewModel>>(_service.GetCategories());
            return View(new CategoryListViewModel
            {
                Categories = categories.Skip((page - 1) * PageSize).Take(PageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = categories.Count()
                }
            });
        }
        // GET: Category/Create
        [HttpGet("Categories/new")]
        public ViewResult Create()
        {
            return View(new CategoryViewModel());
        }

        [HttpPost("Categories/new")]
        public IActionResult Create(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                _service.Create(_mapper.Map<CategoryViewModel, CategoryDTO>(category));
                return RedirectToAction("Index");
            }
            return View();
        }

            // GET: Category/Edit/5
        [HttpGet("Categories/{id}")]
        public ViewResult Edit([FromRoute]int id)
        {
            var category = _mapper.Map<CategoryViewModel>(_service.GetCategory(id));
            return View(category);
        }

        [HttpPost("Categories/{id}")]
        public IActionResult Edit(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                _service.Update((_mapper.Map<CategoryDTO>(category)));
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpDelete("Categories/{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            _service.Delete(id);
            return Ok();
        }

        [HttpGet]
        public ActionResult Childs([FromRoute]int id)
        {
            var categories = _service.GetChildCategories(id);
            return Ok(categories);
        }
        
        [HttpGet]
        public PartialViewResult CategorySelect()
        {
            var categories = _service.GetCategories();

            return PartialView(_mapper.Map<IEnumerable<CategoryViewModel>>(categories));
        }

        [HttpGet]
        public PartialViewResult ParentCategorySelect()
        {
            var categories = _service.GetCategories();

            return PartialView(_mapper.Map<IEnumerable<CategoryViewModel>>(categories));
        }

        [HttpGet]
        public PartialViewResult CategorySingleSelect()
        {
            var categories = _service.GetCategories();

            return PartialView(_mapper.Map<IEnumerable<CategoryViewModel>>(categories));
        }

        [HttpGet]
        public ActionResult CategoryJson()
        {
            var categories = _service.GetCategories()
                .Select(c => new { c.CategoryId, c.Name }).ToList();
            return Ok(new SelectList(categories, "CategoryId", "Name"));
        }

        /// <summary>
        /// Get category info for pie chart
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet("api/categories-chart")]
        [ProducesResponseType(200)]
        [AllowAnonymous]
        public IEnumerable<CategoryPieChartInfoDTO> CategoryWithCountOfProducts()
        {
            return  _service.GetCategoryNameWithCountOfProducts();
        }

        /// <summary>
        /// Get list of categories
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns list of categories</response>
        [HttpGet("api/categories")]
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
        [HttpGet("api/categories/{id}")]
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
        [HttpPost("api/categories/")]
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
        [HttpPatch("api/categories/{id}")]
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
        [HttpDelete("api/categories/{id}")]
        [ProducesResponseType(200)]
        public ActionResult DeleteCategory([FromRoute]int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}