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
            var order = _context.Set<Order>().SingleOrDefault(o => o.OrderId == id);
            var productOrders = _context.Set<ProductOrder>().Where(po => po.OrderId == id).Include(po => po.Product);
            if(productOrders != null)//?
            {
                var productsForUpdate = new List<Product>();
                foreach(var po in productOrders)
                {
                    int newCount = po.OrderCount + po.Product.Count;
                    var product = po.Product;//?
                    product.Count = newCount;
                    productsForUpdate.Add(product);
                }

                _context.Set<Product>().UpdateRange(productsForUpdate);
                _context.SaveChanges();
            }

            _context.Set<Order>().Remove(order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Order Get(int id)
        {
            return _context.Set<Order>().AsNoTracking()
                .Include(o => o.Customer)
                .Include(o => o.ProductOrders)
                .ThenInclude(po => po.Product)
                .SingleOrDefault(o => o.OrderId == id);
        }

        public IQueryable<Order> GetAll()
        {
            return _context.Set<Order>()
                .Include(o => o.Customer)
                .Include(o => o.ProductOrders)
                .ThenInclude(o => o.Product);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Order order)
        {
            _context.Set<Order>().Update(order);
            _context.SaveChanges();
        }
    }
}
