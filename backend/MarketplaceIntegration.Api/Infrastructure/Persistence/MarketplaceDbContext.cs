using MarketplaceIntegration.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceIntegration.Api.Infrastructure.Persistence;

public class MarketplaceDbContext(DbContextOptions<MarketplaceDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
    public DbSet<Channel> Channels => Set<Channel>();
    public DbSet<ChannelAccount> ChannelAccounts => Set<ChannelAccount>();
    public DbSet<ChannelListing> ChannelListings => Set<ChannelListing>();
    public DbSet<Inventory> Inventory => Set<Inventory>();
    public DbSet<SyncJob> SyncJobs => Set<SyncJob>();
    public DbSet<SyncError> SyncErrors => Set<SyncError>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasIndex(x => x.Sku).IsUnique();
        modelBuilder.Entity<ProductVariant>().HasIndex(x => x.Sku).IsUnique();

        modelBuilder.Entity<Product>()
            .HasMany(x => x.Variants)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        modelBuilder.Entity<Channel>()
            .HasMany(x => x.Accounts)
            .WithOne(x => x.Channel)
            .HasForeignKey(x => x.ChannelId);

        modelBuilder.Entity<SyncJob>()
            .HasMany(x => x.Errors)
            .WithOne(x => x.SyncJob)
            .HasForeignKey(x => x.SyncJobId);

        modelBuilder.Entity<ProductVariant>()
            .Property(x => x.Price)
            .HasPrecision(18, 2);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAtUtc = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
