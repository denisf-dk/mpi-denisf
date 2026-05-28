using MarketplaceIntegration.Api.Domain.Enums;

namespace MarketplaceIntegration.Api.Domain.Entities;

public class Channel : BaseEntity
{
    public required string Name { get; set; }
    public ChannelType Type { get; set; }
    public bool IsEnabled { get; set; } = true;

    public ICollection<ChannelAccount> Accounts { get; set; } = new List<ChannelAccount>();
}
