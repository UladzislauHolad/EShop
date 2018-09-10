using EShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.EF.ModelConfiguration
{
    public class OrderStatusChangeConfiguration : IEntityTypeConfiguration<OrderStatusChange>
    {
        public void Configure(EntityTypeBuilder<OrderStatusChange> builder)
        {
            builder.ToTable("OrderStatusChanges")
                .HasOne(os => os.Order)
                .WithMany(o => o.OrderStatusChanges)
                .HasForeignKey(os => os.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
