using MarketplaceIntegration.Api.Domain.Entities;
using MarketplaceIntegration.Api.Domain.Enums;

namespace MarketplaceIntegration.Api.Infrastructure.Connectors;

public interface IMarketplaceConnector
{
    ChannelType SupportedChannel { get; }
    Task SyncProductAsync(Product product, CancellationToken cancellationToken = default);
}
