﻿using EShop.Data.EF;
using EShop.Data.EF.Interfaces;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Repositories
{
    public class ProductOrderRepository : IRepository<ProductOrder>
    {
        IDbContext _context;

        public ProductOrderRepository(IDbContext context)
        {
            _context = context;
        }

        public void Create(ProductOrder item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var productOrder = _context.Set<ProductOrder>().Single(po => po.ProductOrderId == id);
            var product = _context.Set<Product>().Single(p => p.ProductId == productOrder.ProductId);
            int updatedCount = product.Count + productOrder.OrderCount;
            product.Count = updatedCount;

            _context.Set<ProductOrder>().Remove(productOrder);
            _context.SaveChanges();

            _context.Set<Product>().Update(product);
            _context.SaveChanges();
        }

        public IEnumerable<ProductOrder> Find(Func<ProductOrder, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public ProductOrder Get(int id)
        {
            return _context.Set<ProductOrder>().SingleOrDefault(po => po.ProductOrderId == id);
        }

        public IQueryable<ProductOrder> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(ProductOrder productOrder)
        {
            ProductOrder existedProductOrder = _context.Set<ProductOrder>()
                .Include(po => po.Product)
                .SingleOrDefault(po => po.ProductOrderId == productOrder.ProductOrderId);

            if (existedProductOrder != null)
            {
                var oldProduct = existedProductOrder.Product;

                if (oldProduct.ProductId == productOrder.ProductId)
                {
                    int newCountForOldProduct = oldProduct.Count + existedProductOrder.OrderCount - productOrder.OrderCount;
                    oldProduct.Count = newCountForOldProduct;

                    existedProductOrder.Product = oldProduct;
                    existedProductOrder.ProductId = productOrder.ProductId;
                    existedProductOrder.Name = productOrder.Name;
                    existedProductOrder.Description = productOrder.Description;
                    existedProductOrder.Price = productOrder.Price;
                    existedProductOrder.OrderCount = productOrder.OrderCount;
                    existedProductOrder.Count = productOrder.Count;

                    _context.SaveChanges();
                }
                else
                {
                    var newProduct = _context.Set<Product>().SingleOrDefault(p => p.ProductId == productOrder.ProductId);

                    if (newProduct != null)
                    {
                        int newCountForOldProduct = oldProduct.Count + existedProductOrder.OrderCount;
                        oldProduct.Count = newCountForOldProduct;

                        int newCountForNewProduct = newProduct.Count - productOrder.OrderCount;
                        newProduct.Count = newCountForNewProduct;

                        existedProductOrder.Product = newProduct;
                        existedProductOrder.ProductId = productOrder.ProductId;
                        existedProductOrder.Name = productOrder.Name;
                        existedProductOrder.Description = productOrder.Description;
                        existedProductOrder.Price = productOrder.Price;
                        existedProductOrder.OrderCount = productOrder.OrderCount;
                        existedProductOrder.Count = productOrder.Count;

                        _context.SaveChanges();
                    }
                }
            }
        }
    }
}
