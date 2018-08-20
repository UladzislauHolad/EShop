using EShop.Data.EF.Interfaces;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EShop.Data.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        IDbContext _context;

        public OrderRepository(IDbContext context)
        {
            _context = context;
        }

        public void Create(Order order)
        {
            _context.Set<Order>().Add(order);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Order Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> GetAll()
        {
            return _context.Set<Order>()
                .Include(o => o.ProductOrders)
                .ThenInclude(o => o.Product);
        }

        public void Update(Order item)
        {
            throw new NotImplementedException();
        }
    }
}
