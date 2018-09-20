using EShop.Data.EF;
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

        public ProductOrder Create(ProductOrder productOrder)
        {
            _context.Set<ProductOrder>().Add(productOrder);
            _context.SaveChanges();

            return productOrder;
        }

        public void Delete(int id)
        {
            var productOrder = _context.Set<ProductOrder>().Single(po => po.ProductOrderId == id);
            _context.Set<ProductOrder>().Remove(productOrder);
            _context.SaveChanges();
        }

        public IEnumerable<ProductOrder> Find(Func<ProductOrder, bool> predicate)
        {
            return _context.Set<ProductOrder>().Include(po => po.Product).Where(predicate).DefaultIfEmpty();
        }

        public ProductOrder Get(int id)
        {
            return _context.Set<ProductOrder>().Include(po => po.Product).SingleOrDefault(po => po.ProductOrderId == id);
        }

        public IQueryable<ProductOrder> GetAll()
        {
            return _context.Set<ProductOrder>();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(ProductOrder productOrder)
        {
            _context.Set<ProductOrder>().Update(productOrder);
            _context.SaveChanges();
        }
    }
}
