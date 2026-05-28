namespace MarketplaceIntegration.Api.Domain.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public required string Sku { get; set; }
    public string? Description { get; set; }

    public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
}
