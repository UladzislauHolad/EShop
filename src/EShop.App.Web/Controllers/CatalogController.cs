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

        /// <summary>
        /// Gets a list of CategoryNestedNodeViewModels.
        /// </summary>
        /// <returns>A list of CategoryNestedNodeViewModels</returns>
        /// <response code="200">Returns the list of CategoryNestedNodeViewModels</response>
        [HttpGet("api/catalog/category-nodes")]
        [ProducesResponseType(200)]
        [AllowAnonymous]
        public IEnumerable<CategoryNestedNodeViewModel> GetCategoryNestedNodes()
        {
            var result = _mapper.Map<IEnumerable<CategoryNestedNodeViewModel>>(_service.GetCategoryNestedNodes());
            
            return result;
        }
    }
}
