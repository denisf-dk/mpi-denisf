namespace MarketplaceIntegration.Api.Domain.Entities;

public class ChannelAccount : BaseEntity
{
    public Guid ChannelId { get; set; }
    public Channel? Channel { get; set; }

    public required string AccountName { get; set; }
    public string? ExternalAccountId { get; set; }
}
