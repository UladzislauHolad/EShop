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

        public ProductContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=productdb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 5, Name = "P1", Price = 321, Description = "Des1" },
                new Product { ProductId = 6, Name = "P2", Price = 21, Description = "Des2" },
                new Product { ProductId = 7, Name = "P3", Price = 31, Description = "Des3" },
                new Product { ProductId = 8, Name = "P4", Price = 121, Description = "Des4" });
        }

    }
}
