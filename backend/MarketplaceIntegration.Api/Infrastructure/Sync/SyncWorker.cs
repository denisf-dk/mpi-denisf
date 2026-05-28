using MarketplaceIntegration.Api.Domain.Entities;
using MarketplaceIntegration.Api.Domain.Enums;
using MarketplaceIntegration.Api.Infrastructure.Connectors;
using MarketplaceIntegration.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceIntegration.Api.Infrastructure.Sync;

public class SyncWorker(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<SyncWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var queue = scope.ServiceProvider.GetRequiredService<ISyncJobQueue>();
            var dbContext = scope.ServiceProvider.GetRequiredService<MarketplaceDbContext>();
            var connectors = scope.ServiceProvider.GetServices<IMarketplaceConnector>().ToDictionary(x => x.SupportedChannel, x => x);

            if (!queue.TryDequeue(out var syncJobId))
            {
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
                continue;
            }

            var job = await dbContext.SyncJobs.FirstOrDefaultAsync(x => x.Id == syncJobId, stoppingToken);
            if (job is null)
            {
                continue;
            }

            try
            {
                job.Status = SyncJobStatus.Running;
                await dbContext.SaveChangesAsync(stoppingToken);

                var channels = await dbContext.Channels.Where(x => x.IsEnabled).ToListAsync(stoppingToken);
                var products = await dbContext.Products.ToListAsync(stoppingToken);

                foreach (var channel in channels)
                {
                    if (!connectors.TryGetValue(channel.Type, out var connector))
                    {
                        continue;
                    }

                    foreach (var product in products)
                    {
                        await connector.SyncProductAsync(product, stoppingToken);
                    }
                }

                job.Status = SyncJobStatus.Completed;
                await dbContext.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Sync job {SyncJobId} failed", syncJobId);

                job.Status = SyncJobStatus.Failed;
                dbContext.SyncErrors.Add(new SyncError
                {
                    SyncJobId = syncJobId,
                    Code = "SYNC_FAILED",
                    Message = ex.Message
                });
                await dbContext.SaveChangesAsync(stoppingToken);
            }
        }
    }
}
