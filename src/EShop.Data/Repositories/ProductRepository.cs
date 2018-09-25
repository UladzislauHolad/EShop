using EShop.Data.EF.Interfaces;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Data.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private IDbContext _context;

        public ProductRepository(IDbContext context)
        {
            _context = context;
        }

        public Product Create(Product product)
        {
            _context.Set<Product>().Add(product);
            _context.SaveChanges();

            return product;
        }

        public void Delete(int id)
        {
            Product p = _context.Set<Product>().First(pr => pr.ProductId == id);
            if(p != null)
            {
                _context.Set<Product>().Remove(p);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return _context.Set<Product>().Where(predicate).ToList();
        }

        public Product Get(int id)
        {
            return _context.Set<Product>()
                .Include(p => p.ProductCategories)
                .ThenInclude(p => p.Category)
                .SingleOrDefault(p => p.ProductId == id);
        }

        public IQueryable<Product> GetAll()
        {
            var prods = (_context.Set<Product>()
                .Include(p => p.ProductCategories)
                    .ThenInclude(p => p.Category));
                
            return prods;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Product Update(Product product)
        {
            var existCategories = _context.Set<ProductCategory>().Where(c => c.ProductId == product.ProductId);
            var existedProduct = _context.Set<Product>().Single(p => p.ProductId == product.ProductId);
            existedProduct.ProductCategories = product.ProductCategories.Except(existCategories).ToList();
            existedProduct.Name = product.Name;
            existedProduct.Price = product.Price;
            existedProduct.Description = product.Description;
            existedProduct.Count = product.Count;
            _context.SaveChanges();

            return existedProduct;
        }
    }
}
