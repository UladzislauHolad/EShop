using EShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.EF.ModelConfiguration
{
    class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasKey(pc => new { pc.ProductId, pc.OrderId });

            builder.HasOne(pc => pc.Product)
                .WithMany(p => p.ProductOrders)
                .HasForeignKey(pt => pt.OrderId);

            builder.HasOne(pc => pc.Order)
                .WithMany(c => c.ProductOrders)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
