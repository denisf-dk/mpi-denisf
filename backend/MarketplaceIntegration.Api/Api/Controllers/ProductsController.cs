using MarketplaceIntegration.Api.Application.DTOs;
using MarketplaceIntegration.Api.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceIntegration.Api.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(ProductService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await service.GetByIdAsync(id, cancellationToken);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateRequest request, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(request, cancellationToken);
        if (!result.Success)
        {
            return ValidationProblem(detail: result.Error, title: "Product validation failed");
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Product!.Id }, result.Product);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdateRequest request, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(id, request, cancellationToken);
        if (!result.Success)
        {
            return NotFound(new { message = result.Error });
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        => await service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}
