using EShop.Data.EF;
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
        private ProductContext db;

        public ProductRepository(ProductContext context)
        {
            db = context;
        }

        public void Create(Product item)
        {
            db.Set<Product>().Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Product p = db.Products.First(pr => pr.ProductId == id);
            if(p != null)
            {
                db.Products.Remove(p);
                db.SaveChanges();
            }
        }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return db.Products.Where(predicate).ToList();
        }

        public Product Get(int id)
        {
            return db.Set<Product>().First(p=>p.ProductId == id);
        }

        public IEnumerable<Product> GetAll()
        {
            var prods = (db.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(p => p.Category));
                
            return prods;
        }

        public void Update(Product item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
