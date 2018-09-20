using EShop.Data.EF.Interfaces;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Repositories
{
    public class OrderStatusChangeRepository : IRepository<OrderStatusChange>
    {
        readonly IDbContext _context;

        public OrderStatusChangeRepository(IDbContext context)
        {
            _context = context;
        }

        public void Create(OrderStatusChange item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderStatusChange> Find(Func<OrderStatusChange, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public OrderStatusChange Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<OrderStatusChange> GetAll()
        {
            return _context.Set<OrderStatusChange>();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(OrderStatusChange item)
        {
            throw new NotImplementedException();
        }

        OrderStatusChange IRepository<OrderStatusChange>.Create(OrderStatusChange item)
        {
            throw new NotImplementedException();
        }
    }
}
