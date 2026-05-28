using MarketplaceIntegration.Api.Domain.Entities;
using MarketplaceIntegration.Api.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace MarketplaceIntegration.Api.Infrastructure.Connectors;

public class EbayMarketplaceConnector(ILogger<EbayMarketplaceConnector> logger) : IMarketplaceConnector
{
    public ChannelType SupportedChannel => ChannelType.Ebay;

    public Task SyncProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Stub sync to eBay for product {Sku}", product.Sku);
        return Task.CompletedTask;
    }
}
