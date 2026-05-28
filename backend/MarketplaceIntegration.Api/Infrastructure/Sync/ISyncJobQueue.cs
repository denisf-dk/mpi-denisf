namespace MarketplaceIntegration.Api.Infrastructure.Sync;

public interface ISyncJobQueue
{
    void Enqueue(Guid syncJobId);
    bool TryDequeue(out Guid syncJobId);
}
