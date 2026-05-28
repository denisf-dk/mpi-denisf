using MarketplaceIntegration.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceIntegration.Api.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChannelListingsController(MarketplaceDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await context.ChannelListings.AsNoTracking().ToListAsync(cancellationToken));
}
