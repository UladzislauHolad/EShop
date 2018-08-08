using EShop.Data.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.EF
{
    public class ProductContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public ProductContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
