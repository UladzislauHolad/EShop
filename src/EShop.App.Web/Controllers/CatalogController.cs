﻿using AutoMapper;
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
    }
}
