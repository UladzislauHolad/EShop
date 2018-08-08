﻿using EShop.Services.DTO;
using EShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using EShop.Data.Interfaces;
using EShop.Data.Entities;
using EShop.Services.Infrastructure;
using AutoMapper;
using EShop.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EShop.Services.Services
{
    public class Service : IService
    {
        IRepository<Product> Db { get; set; }

        public Service(IRepository<Product> repository)
        {
            Db = repository;
        }

        public ProductDTO GetProduct(int? id)
        {
            if(id == null)
            {
                throw new ValidationException("Не установлен id продукта", "");
            }
            Product p = Db.Get(id.Value);
            if(p == null)
            {
                throw new ValidationException("Продукт не найден", "");
            }

            return new ProductDTO { ProductId = p.ProductId, Name = p.Name, Price = p.Price, Description = p.Description};
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(Db.GetAll());
        }

        public void Add(ProductDTO product)
        {
            Db.Create(new Product { Name = product.Name, Price = product.Price, Description = product.Description });
        }
    }
}
