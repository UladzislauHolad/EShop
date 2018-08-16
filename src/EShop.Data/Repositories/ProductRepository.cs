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

        public void Create(Product product)
        {
            foreach (var category in product.ProductCategories)
            {
                category.ProductId = product.ProductId;
            }
            _context.Set<Product>().Add(product);
            _context.SaveChanges();
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
                .First(p => p.ProductId == id);
        }

        public IQueryable<Product> GetAll()
        {
            var prods = (_context.Set<Product>()
                .Include(p => p.ProductCategories)
                    .ThenInclude(p => p.Category));
                
            return prods;
        }

        public void Update(Product product)
        {

            foreach (var category in product.ProductCategories)
            {
                category.ProductId = product.ProductId;
            }
            var oldCat = _context.Set<ProductCategory>().Where(pc => pc.ProductId == product.ProductId);
            _context.Set<ProductCategory>().RemoveRange(oldCat);
            _context.Set<Product>().Update(product);
            _context.SaveChanges();
        }
    }
}
