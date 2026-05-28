using MarketplaceIntegration.Api.Domain.Entities;
using MarketplaceIntegration.Api.Domain.Enums;
using MarketplaceIntegration.Api.Infrastructure.Persistence;
using MarketplaceIntegration.Api.Infrastructure.Sync;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceIntegration.Api.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SyncJobsController(MarketplaceDbContext context, ISyncJobQueue queue) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await context.SyncJobs.AsNoTracking().OrderByDescending(x => x.CreatedAtUtc).ToListAsync(cancellationToken));

    [HttpPost("trigger")]
    public async Task<IActionResult> Trigger(CancellationToken cancellationToken)
    {
        var job = new SyncJob
        {
            JobType = "ManualSync",
            Status = SyncJobStatus.Queued
        };

        context.SyncJobs.Add(job);
        await context.SaveChangesAsync(cancellationToken);
        queue.Enqueue(job.Id);

        return Accepted(new { job.Id, job.Status, message = "Sync job queued." });
    }
}
