namespace MarketplaceIntegration.Api.Domain.Entities;

public class SyncError : BaseEntity
{
    public Guid SyncJobId { get; set; }
    public SyncJob? SyncJob { get; set; }

    public string Code { get; set; } = "UNKNOWN";
    public required string Message { get; set; }
}
