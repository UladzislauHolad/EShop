using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.App.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _service;
        public int PageSize = 10;

        public CategoryController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: Category
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

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new CategoryViewModel());
        }

        [HttpPost]
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
        public ActionResult Edit(int id)
        {
            var category = _mapper.Map<CategoryViewModel>(_service.GetCategory(id));
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                _service.Update((_mapper.Map<CategoryDTO>(category)));
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult Childs(int id)
        {
            var categories = _service.GetChildCategories(id);
            return Json(categories);
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
    }
}