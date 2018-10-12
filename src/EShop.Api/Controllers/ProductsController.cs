using AutoMapper;
using EShop.Api.Models.ProductsViewModels;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private IProductService _service;
        private readonly IMapper _mapper;

        public ProductsController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets list of ProductViewModel
        /// </summary>
        /// <returns>List of ProductViewModel</returns>
        /// <response code="200">Returns the list of ProductViewModel</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public IEnumerable<ProductTableViewModel> GetProducts()
        {
            return _mapper.Map<IEnumerable<ProductTableViewModel>>(_service.GetProducts());
        }
    }
}
