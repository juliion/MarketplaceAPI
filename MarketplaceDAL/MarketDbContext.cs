using MarketplaceDAL.Configurations;
using MarketplaceDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceDAL
{
    public class MarketDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public MarketDbContext(DbContextOptions<MarketDbContext> opt)
            : base(opt) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new SaleConfiguration());
        }
    }
}
