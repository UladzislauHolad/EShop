using AutoMapper;
using EShop.App.Web.Models;
using EShop.App.Web.Models.Angular.Catalog;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Controllers
{
    [Authorize]
    public class CatalogController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _service;

        public CatalogController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public ViewResult Index()
        {
            var categories = _mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryViewModel>>(_service.GetChildCategories(0));
            return View(categories);
        }

        [HttpGet("api/catalog/category-nodes/{id}")]
        [AllowAnonymous]
        public ActionResult GetCategoryNodes([FromRoute]int id)
        {
            var result = _service.GetCategoryNodes(id);

            if(result.Count() == 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
