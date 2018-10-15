using AutoMapper;
using EShop.App.Web.Models;
using EShop.App.Web.Models.Angular.Product;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace EShop.App.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private IProductService _service;
        private readonly IMapper _mapper;
        public int PageSize = 8;

        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("")]
        [HttpGet("Products")]
        [HttpGet("Products/Pages/{page}")]
        public ViewResult Index([FromRoute]int page = 1)
        {
            IEnumerable<ProductDTO> productDtos = _service.GetProducts();
            var Products = _mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductViewModel>>(productDtos);

            return View(new ProductListViewModel
            {
                Products = Products.Skip((page-1)*PageSize).Take(PageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = Products.Count()
                }
            });
        }

        [HttpGet("Products/new")]
        public ViewResult AddProduct()
        {
            return View(new ProductViewModel());
        }

        [HttpPost("Products/new")]
        public IActionResult AddProduct(ProductViewModel product)
        {
            if(ModelState.IsValid)
            {
                product.Categories = new List<CategoryViewModel>();
                foreach (var categoryId in product.CategoriesId)
                {
                    product.Categories.Add(new CategoryViewModel { CategoryId = categoryId });
                }
                _service.Add(_mapper.Map<ProductViewModel, ProductDTO>(product));
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet("Products/{id}")]
        public ViewResult Edit([FromRoute]int id)
        {
            var product = _mapper.Map<ProductDTO, ProductViewModel>(_service.GetProduct(id));
            return View(product);
        }

        //[HttpPost("Products/{id}")]
        //public IActionResult Edit(ProductViewModel product)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        if(product.CategoriesId != null)
        //        {
        //            product.Categories = new List<CategoryViewModel>();
        //            foreach (var categoryId in product.CategoriesId)
        //            {
        //                product.Categories.Add(new CategoryViewModel { CategoryId = categoryId });
        //            }
        //        }
        //        _service.Update((_mapper.Map<ProductViewModel, ProductDTO>(product)));
        //        return RedirectToAction("Index");
        //    }
        //    return View(product);
        //}

        [HttpDelete("Products/{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            _service.Delete(id);
            return Ok();
        }

        [HttpGet]
        public PartialViewResult Products(int id)
        {
            var products = _service.GetProductsByCategoryId(id);

            return PartialView(_mapper.Map<IEnumerable<ProductViewModel>>(products));
        }

        public PartialViewResult Categories(IEnumerable<CategoryViewModel> categories)
        {
            return PartialView(categories);
        }

        public ActionResult ChartData()
        {
            var data = _service.GetCategoriesWithCountOfProducts();

            return Ok(data);
        }

        [HttpGet]
        public PartialViewResult ProductSelect(int id)
        {
            var products = _service.GetProducts().Where(p => p.Categories.Where(c => c.CategoryId == id) != null);

            return PartialView(_mapper.Map<IEnumerable<ProductViewModel>>(products));
        }

        public PartialViewResult Product(int id)
        {
            var product = _mapper.Map<ProductViewModel>(_service.GetProduct(id));

            return PartialView(product);
        }

        [HttpGet]
        public ActionResult ProductJson(int id)
        {
            var products = _service.GetProducts()
                .Where(p => p.Categories
                    .Any(c => c.CategoryId == id))
                .Select(p => new { p.ProductId, p.Name }).ToList();
            return Ok(new SelectList(products, "ProductId", "Name"));
        }

        /// <summary>
        /// Gets list of ProductViewModel
        /// </summary>
        /// <returns>List of ProductViewModel</returns>
        /// <response code="200">Returns the list of ProductViewModel</response>
        [HttpGet("api/products")]
        [ProducesResponseType(200)]
        public IEnumerable<ProductViewModel> GetProducts()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(_service.GetProducts());
        }

        /// <summary>
        /// Get ProductViewModel by id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Get api/products/1
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>ProductViewModel</returns>
        [HttpGet("api/products/{id}")]
        [AllowAnonymous]
        public ActionResult GetProducts([FromRoute]int id)
        {
            return Ok(_service.GetProduct(id));
        }

        /// <summary>
        /// Create new instance of ProductViewModel
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Post api/products
        ///     {
        ///         "name": "Product",
        ///         "price": 122,
        ///         "description": "Description",
        ///         "count": 2,
        ///         "categories": [
        ///             {
        ///                 "categoryId": 0,
        ///                 "name": "string",
        ///                 "parentId": 0
        ///             }
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <response code="201">ProductViewModel is created</response>
        /// <response code="400">ProductViewModel is not valid</response>
        [HttpPost("api/products")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public ActionResult<ProductViewModel> CreateProduct([FromBody]ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _service.Add(_mapper.Map<ProductViewModel, ProductDTO>(product));
                return StatusCode(StatusCodes.Status201Created, null);
            }
            return BadRequest();
        }

        ///// <summary>
        ///// Update instance of ProductViewModel
        ///// </summary>
        ///// <remarks>
        ///// 
        ///// Sample request:
        ///// 
        /////     Patch api/products/1
        /////     {
        /////         "name": "Product",
        /////         "price": 122,
        /////         "description": "Description",
        /////         "count": 2,
        /////         "categories": [
        /////             {
        /////                 "categoryId": 0,
        /////                 "name": "string",
        /////                 "parentId": 0
        /////             }
        /////         ]
        /////     }
        ///// 
        ///// </remarks>
        ///// <param name="id"></param>
        ///// <param name="product"></param>
        ///// <returns></returns>
        ///// <response code="204">ProductViewModel is updated</response>
        ///// <response code="400">ProductViewModel is not valid</response>
        //[HttpPatch("api/products/{id}")]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //[AllowAnonymous]
        //public ActionResult UpdateProduct([FromRoute]int id, [FromBody]ProductViewModel product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _service.Update((_mapper.Map<ProductViewModel, ProductDTO>(product)));
        //        return NoContent();
        //    }
        //    return BadRequest();
        //}

        /// <summary>
        /// Delete ProductViewModel by id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Delete api/products/1
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>ProductViewModel</returns>
        /// <response code="200">ProductViewModel is deleted</response>
        [HttpDelete("api/products/{id}")]
        [ProducesResponseType(200)]
        [AllowAnonymous]
        public ActionResult DeleteProduct([FromRoute]int id)
        {
            _service.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Get list of ProductAngularViewModel by category id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Get api/categories/1/products
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>ProductViewModel</returns>
        /// <response code="200">Returns list of ProductAngularViewModel by category id</response>
        [HttpGet("api/categories/{id}/products")]
        [ProducesResponseType(200)]
        [AllowAnonymous]
        public IEnumerable<ProductAngularViewModel> GetProductOfCategory([FromRoute]int id)
        {
            var products = _mapper.Map<IEnumerable<ProductAngularViewModel>>(_service.GetProductsByCategoryId(id));

            return products;
        }
    }
}
