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
    public class PaymentMethodRepository : IRepository<PaymentMethod>
    {
        IDbContext _context;

        public PaymentMethodRepository(IDbContext  context)
        {
            _context = context;
        }

        public void Create(PaymentMethod item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PaymentMethod> Find(Func<PaymentMethod, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public PaymentMethod Get(int id)
        {
            return _context.Set<PaymentMethod>()
                .AsNoTracking()
                .SingleOrDefault(p => p.PaymentMethodId == id);
        }

        public IQueryable<PaymentMethod> GetAll()
        {
            return _context.Set<PaymentMethod>();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public PaymentMethod Update(PaymentMethod item)
        {
            throw new NotImplementedException();
        }

        PaymentMethod IRepository<PaymentMethod>.Create(PaymentMethod item)
        {
            throw new NotImplementedException();
        }
    }
}
