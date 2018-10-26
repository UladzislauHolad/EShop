﻿using AutoMapper;
using EShop.Api.Infrastructure;
using EShop.Api.Models.ProductsViewModels;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Controllers
{
    //[Authorize]
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
        /// Gets list of Products
        /// </summary>
        /// <returns>List of ProductViewModel</returns>
        /// <response code="200">Returns the list of ProductViewModel</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<IEnumerable<ProductViewModel>>(_service.GetProducts()).AsQueryable());
        }

        /// <summary>
        /// Get Product by id
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
        public ProductViewModel GetProduct([FromRoute]int id)
        {
            return _mapper.Map<ProductViewModel>(_service.GetProduct(id));
        }

        /// <summary>
        /// Create new instance of Product
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
        /// <response code="201">Product is created</response>
        /// <response code="422">Product is not valid</response>
        [HttpPost("api/products")]
        [ProducesResponseType(201)]
        [ProducesResponseType(422)]
        public ActionResult CreateProduct([FromBody]CreateProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _service.Add(_mapper.Map<ProductDTO>(product));
                return StatusCode(StatusCodes.Status201Created);
            }

            return StatusCode(StatusCodes.Status422UnprocessableEntity, ModelState.ToErrorsStringArray());
        }

        /// <summary>
        /// Update instance of Product
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Patch api/products/1
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
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <response code="204">Product is updated</response>
        /// <response code="422">Product is not valid</response>
        [HttpPatch("api/products/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(422)]
        public ActionResult UpdateProduct([FromRoute]int id, [FromBody]UpdateProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _service.Update(id, _mapper.Map<ProductDTO>(product));
                return NoContent();
            }

            return StatusCode(StatusCodes.Status422UnprocessableEntity, ModelState.ToErrorsStringArray());
        }

        /// <summary>
        /// Delete Product by id
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
        /// <response code="200">Product is deleted</response>
        [HttpDelete("api/products/{id}")]
        [ProducesResponseType(200)]
        public ActionResult DeleteProduct([FromRoute]int id)
        {
            _service.Delete(id);
            return Ok();
        }

        ///// <summary>
        ///// Get list of Products by category id
        ///// </summary>
        ///// <remarks>
        ///// 
        ///// Sample request:
        ///// 
        /////     Get api/categories/1/products
        /////     
        ///// </remarks>
        ///// <param name="id"></param>
        ///// <returns>Product</returns>
        ///// <response code="200">Returns list of Products by category id</response>
        //[HttpGet("/api/categories/{id}/products")]
        //[ProducesResponseType(200)]
        //public IEnumerable<ProductTableViewModel> GetProductOfCategory([FromRoute]int id)
        //{
        //    var products = _mapper.Map<IEnumerable<ProductTableViewModel>>(_service.GetProductsByCategoryId(id));

        //    return products;
        //}
    }
}
