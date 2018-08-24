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
            //builder.HasOne(po => po.Product)
            //    .WithOne(p => p.ProductOrder)
            //    .HasForeignKey<Product>(l => l.ProductForeignKey);
        }

    }
}
