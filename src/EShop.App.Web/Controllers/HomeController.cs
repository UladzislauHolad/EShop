using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Controllers
{
    public class HomeController : Controller
    {
        private IService _service;
        private readonly IMapper _mapper;

        public HomeController(IService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public ViewResult Index()
        {
            IEnumerable<ProductDTO> productDtos = _service.GetProducts();
            var Products = _mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductViewModel>>(productDtos);
            return View(Products);
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
                _service.Add(_mapper.Map<ProductViewModel, ProductDTO>(product));
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
