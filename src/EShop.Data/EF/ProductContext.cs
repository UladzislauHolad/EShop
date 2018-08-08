using EShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.EF
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
