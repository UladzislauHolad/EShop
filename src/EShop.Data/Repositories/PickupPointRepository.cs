using EShop.Data.EF.Interfaces;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Repositories
{
    public class PickupPointRepository : IRepository<PickupPoint>
    {
        IDbContext _context;

        public PickupPointRepository(IDbContext context)
        {
            _context = context;
        }

        public void Create(PickupPoint item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PickupPoint> Find(Func<PickupPoint, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public PickupPoint Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PickupPoint> GetAll()
        {
            return _context.Set<PickupPoint>();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public PickupPoint Update(PickupPoint item)
        {
            throw new NotImplementedException();
        }

        PickupPoint IRepository<PickupPoint>.Create(PickupPoint item)
        {
            throw new NotImplementedException();
        }
    }
}
