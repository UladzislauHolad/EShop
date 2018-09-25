using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("Products/{id}")]
        public IActionResult Edit(ProductViewModel product)
        {
            if(ModelState.IsValid)
            {
                if(product.CategoriesId != null)
                {
                    product.Categories = new List<CategoryViewModel>();
                    foreach (var categoryId in product.CategoriesId)
                    {
                        product.Categories.Add(new CategoryViewModel { CategoryId = categoryId });
                    }
                }
                _service.Update((_mapper.Map<ProductViewModel, ProductDTO>(product)));
                return RedirectToAction("Index");
            }
            return View(product);
        }

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

        [HttpGet("api/products")]
        public ActionResult GetProducts()
        {
            return Ok(_service.GetProducts());
        }

        [HttpGet("api/products/{id}")]
        public ActionResult GetProducts([FromRoute]int id)
        {
            return Ok(_service.GetProduct(id));
        }

        [HttpPost("api/products")]
        [AllowAnonymous]
        public ActionResult CreateProduct([FromBody]ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _service.Add(_mapper.Map<ProductViewModel, ProductDTO>(product));
                return Ok();
            }
            return BadRequest();
        }

        [HttpPatch("api/products/{id}")]
        [AllowAnonymous]
        public ActionResult UpdateProduct([FromRoute]int id, [FromBody]ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _service.Update((_mapper.Map<ProductViewModel, ProductDTO>(product)));
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("api/products/{id}")]
        [AllowAnonymous]
        public ActionResult DeleteProduct([FromRoute]int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}
