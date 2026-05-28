using MarketplaceIntegration.Api.Application.DTOs;
using MarketplaceIntegration.Api.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceIntegration.Api.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChannelsController(ChannelService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var channel = await service.GetByIdAsync(id, cancellationToken);
        return channel is null ? NotFound() : Ok(channel);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ChannelCreateRequest request, CancellationToken cancellationToken)
    {
        var channel = await service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = channel.Id }, channel);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ChannelUpdateRequest request, CancellationToken cancellationToken)
        => await service.UpdateAsync(id, request, cancellationToken) ? NoContent() : NotFound();

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        => await service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}
