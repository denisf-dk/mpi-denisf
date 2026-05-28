using MarketplaceIntegration.Api.Application.Abstractions;
using MarketplaceIntegration.Api.Application.DTOs;
using MarketplaceIntegration.Api.Domain.Entities;

namespace MarketplaceIntegration.Api.Application.Services;

public class ProductService(IProductRepository repository)
{
    public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default) => repository.GetAllAsync(cancellationToken);

    public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => repository.GetByIdAsync(id, cancellationToken);

    public async Task<(bool Success, string? Error, Product? Product)> CreateAsync(ProductCreateRequest request, CancellationToken cancellationToken = default)
    {
        if (await repository.GetBySkuAsync(request.Sku, cancellationToken) is not null)
        {
            return (false, "Product SKU already exists.", null);
        }

        var entity = new Product
        {
            Name = request.Name,
            Sku = request.Sku,
            Description = request.Description
        };

        await repository.AddAsync(entity, cancellationToken);
        return (true, null, entity);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(Guid id, ProductUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return (false, "Product not found.");
        }

        entity.Name = request.Name;
        entity.Description = request.Description;

        await repository.UpdateAsync(entity, cancellationToken);
        return (true, null);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        await repository.DeleteAsync(entity, cancellationToken);
        return true;
    }
}
