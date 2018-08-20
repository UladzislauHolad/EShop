using EShop.Data.EF.Interfaces;
using EShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data.EF
{
    public class EShopContext : DbContext, IDbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ProductOrder> ProductOrders { get; set; }

        public EShopContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
   }
}
