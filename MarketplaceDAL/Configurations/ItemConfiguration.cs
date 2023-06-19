using MarketplaceDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceDAL.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(item => item.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(item => item.Description)
                .IsRequired();
            builder.Property(item => item.Metadata)
                .IsRequired();

            builder.HasIndex(item => item.Name).IsUnique();
        }
    }
}
