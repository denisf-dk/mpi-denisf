using MarketplaceIntegration.Api.Domain.Entities;
using MarketplaceIntegration.Api.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace MarketplaceIntegration.Api.Infrastructure.Connectors;

public class AmazonMarketplaceConnector(ILogger<AmazonMarketplaceConnector> logger) : IMarketplaceConnector
{
    public ChannelType SupportedChannel => ChannelType.Amazon;

    public Task SyncProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Stub sync to Amazon for product {Sku}", product.Sku);
        return Task.CompletedTask;
    }
}
