﻿using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EShop.App.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _service;
        private readonly IMapper _mapper;
        public int PageSize = 10;

        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public ViewResult Index(int page = 1)
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

        [HttpGet]
        public ViewResult AddProduct()
        {
            return View(new ProductViewModel());
        }

        [HttpPost]
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

        [HttpGet]
        public ViewResult Edit(int? id)
        {
            var product = _mapper.Map<ProductDTO, ProductViewModel>(_service.GetProduct(id));
            return View(product);
        }

        [HttpPost]
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
            return View();
        }

        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
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
    }
}
