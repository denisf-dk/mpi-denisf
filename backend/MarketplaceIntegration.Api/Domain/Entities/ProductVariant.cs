namespace MarketplaceIntegration.Api.Domain.Entities;

public class ProductVariant : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public required string Sku { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
}
