using EShop.Data.EF.Interfaces;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        IDbContext _context;

        public CustomerRepository(IDbContext context)
        {
            _context = context;
        }

        public Customer Create(Customer item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var customer = _context.Set<Customer>().SingleOrDefault(c => c.CustomerId == id);
            if(customer != null)
            {
                _context.Set<Customer>().Remove(customer);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Customer> Find(Func<Customer, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            return _context.Set<Customer>().SingleOrDefault(c => c.CustomerId == id);
        }

        public IQueryable<Customer> GetAll()
        {
            return _context.Set<Customer>();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            _context.Set<Customer>().Update(customer);
            _context.SaveChanges();
        }
    }
}
