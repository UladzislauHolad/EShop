using EShop.Data.EF.Interfaces;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Repositories
{
    public class OrdersRepository : IRepository<Order>
    {
        IDbContext _context;

        public OrdersRepository(IDbContext context)
        {
            _context = context;
        }

        public void Create(Order item)
        {
            throw new NotImplementedException();
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

        public System.Linq.IQueryable<Order> GetAll()
        {
            return _context.Set<Order>()
                .Include(o => o.Products);
        }

        public void Update(Order item)
        {
            throw new NotImplementedException();
        }
    }
}
