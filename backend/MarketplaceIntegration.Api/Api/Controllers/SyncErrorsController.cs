using MarketplaceIntegration.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceIntegration.Api.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SyncErrorsController(MarketplaceDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await context.SyncErrors.AsNoTracking().ToListAsync(cancellationToken));
}
