using MarketplaceIntegration.Api.Application.Abstractions;
using MarketplaceIntegration.Api.Domain.Entities;
using MarketplaceIntegration.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceIntegration.Api.Infrastructure.Repositories;

public class ProductRepository(MarketplaceDbContext context) : IProductRepository
{
    public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        => context.Products.AsNoTracking().ToListAsync(cancellationToken);

    public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default)
        => context.Products.FirstOrDefaultAsync(x => x.Sku == sku, cancellationToken);

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
    {
        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
    }
}
