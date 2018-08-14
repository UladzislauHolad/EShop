using EShop.Data.EF;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Data.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        ProductContext _context;

        public CategoryRepository(ProductContext context)
        {
            _context = context;
        }

        public void Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Category category = _context.Categories.Find(id);
            var childs = _context.Categories.Where(c => c.ParentId == id).ToList();
            _context.Categories.RemoveRange(childs);
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
            return _context.Categories.Where(predicate).ToList();
        }

        public Category Get(int id)
        {
            return _context.Categories.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories;
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
    }
}
