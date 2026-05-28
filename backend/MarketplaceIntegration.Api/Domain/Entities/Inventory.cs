namespace MarketplaceIntegration.Api.Domain.Entities;

public class Inventory : BaseEntity
{
    public Guid ProductVariantId { get; set; }
    public ProductVariant? ProductVariant { get; set; }

    public int Quantity { get; set; }
}
