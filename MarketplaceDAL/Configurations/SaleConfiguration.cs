using MarketplaceDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceDAL.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.Property(sale => sale.ItemId)
                .IsRequired();
            builder.Property(sale => sale.CreatedDt)
                .IsRequired();
            builder.Property(sale => sale.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(sale => sale.Status)
                .IsRequired();
            builder.Property(sale => sale.Seller)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(sale => sale.Buyer)
                .HasMaxLength(100);
            builder.HasOne(sale => sale.Item)
                .WithMany(item => item.Sales)
                .HasForeignKey(sale => sale.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
