using MarketplaceIntegration.Api.Domain.Enums;

namespace MarketplaceIntegration.Api.Domain.Entities;

public class SyncJob : BaseEntity
{
    public string JobType { get; set; } = "FullSync";
    public SyncJobStatus Status { get; set; } = SyncJobStatus.Queued;
    public Guid? ChannelId { get; set; }

    public ICollection<SyncError> Errors { get; set; } = new List<SyncError>();
}
