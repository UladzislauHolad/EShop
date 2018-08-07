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
        private IService service;
        protected HomeController(IService service)
        {
            this.service = service;
        }

        public ViewResult Index()
        {
            IEnumerable<ProductDTO> productDtos = service.GetProducts();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var Products = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);
            return View(Products);
        }
    }
}
