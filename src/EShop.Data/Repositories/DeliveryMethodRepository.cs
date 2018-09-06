using EShop.Data.EF.Interfaces;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Repositories
{
    public class DeliveryMethodRepository : IRepository<DeliveryMethod>
    {
        private readonly IDbContext _context;

        public DeliveryMethodRepository(IDbContext context)
        {
            _context = context;
        }

        public void Create(DeliveryMethod item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeliveryMethod> Find(Func<DeliveryMethod, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public DeliveryMethod Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DeliveryMethod> GetAll()
        {
            return _context.Set<DeliveryMethod>();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(DeliveryMethod item)
        {
            throw new NotImplementedException();
        }
    }
}
