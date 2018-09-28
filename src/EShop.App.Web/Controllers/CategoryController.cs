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

        [HttpGet("api/categories-chart")]
        [AllowAnonymous]
        public ActionResult CategoryWithCountOfProducts()
        {
            var data = _service.GetCategoryNameWithCountOfProducts();

            return Ok(data);
        }

        [HttpGet("api/categories")]
        public ActionResult GetCategories()
        {
            return Ok(_service.GetCategories());
        }

        [HttpGet("api/categories/{id}")]
        public ActionResult GetCategory([FromRoute]int id)
        {
            return Ok(_service.GetCategory(id));
        }

        [HttpPost("api/categories/")]
        public ActionResult CreateCategory([FromBody]CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                _service.Create(_mapper.Map<CategoryDTO>(category));
                return Ok();
            }

            return BadRequest();
        }

        [HttpPatch("api/categories/{id}")]
        public ActionResult UpdateCategory([FromRoute]int id, [FromBody]CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                _service.Update(_mapper.Map<CategoryDTO>(category));
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("api/categories/{id}")]
        public ActionResult DeleteCategory([FromRoute]int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}