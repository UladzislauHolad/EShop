using EShop.Data.EF.Interfaces;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Data.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private IDbContext _context;

        public CategoryRepository(IDbContext context)
        {
            _context = context;
        }

        public Category Create(Category category)
        {
            _context.Set<Category>().Add(category);
            _context.SaveChanges();

            return category;
        }

        public void Delete(int id)
        {
            Category category = _context.Set<Category>().Find(id);
            var childs = _context.Set<Category>().Where(c => c.ParentId == id).ToList();
            _context.Set<Category>().RemoveRange(childs);
            _context.Set<Category>().Remove(category);
            _context.SaveChanges();
        }

        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
            return _context.Set<Category>().Where(predicate).ToList();
        }

        public Category Get(int id)
        {
            return _context.Set<Category>().Find(id);
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Set<Category>();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Set<Category>().Update(category);
            _context.SaveChanges();
        }
    }
}
