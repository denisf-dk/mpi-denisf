namespace MarketplaceIntegration.Api.Domain.Entities;

public class ChannelListing : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public Guid ChannelId { get; set; }
    public Channel? Channel { get; set; }

    public string? ExternalListingId { get; set; }
    public required string Status { get; set; }
}
