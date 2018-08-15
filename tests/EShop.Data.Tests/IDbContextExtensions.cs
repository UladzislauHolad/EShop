using EShop.Data.EF.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Data.Tests
{
    public static class IDbContextExtensions
    {
        
        public static Mock<IDbContext> SetupDbSet<T>(
            this Mock<IDbContext> context, IEnumerable<T> collection)
            where T : class
        {
            var data = collection.ToList();
            var queryable = data.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>()
                .Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            context.Setup(c => c.Set<T>()).Returns(dbSet.Object);

            return context;
        }
    }
}
