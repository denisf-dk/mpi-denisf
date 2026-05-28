using System.Collections.Concurrent;

namespace MarketplaceIntegration.Api.Infrastructure.Sync;

public class InMemorySyncJobQueue : ISyncJobQueue
{
    private readonly ConcurrentQueue<Guid> _queue = new();

    public void Enqueue(Guid syncJobId) => _queue.Enqueue(syncJobId);

    public bool TryDequeue(out Guid syncJobId) => _queue.TryDequeue(out syncJobId);
}
