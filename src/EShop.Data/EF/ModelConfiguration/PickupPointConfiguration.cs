using EShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.EF.ModelConfiguration
{
    public class PickupPointConfiguration : IEntityTypeConfiguration<PickupPoint>
    {
        public void Configure(EntityTypeBuilder<PickupPoint> builder)
        {
            builder
                .ToTable("PickupPoints")
                .HasMany(pp => pp.Orders)
                .WithOne(o => o.PickupPoint)
                .HasForeignKey(o => o.PickupPointId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
