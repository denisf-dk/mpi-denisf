using MarketplaceIntegration.Api.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceIntegration.Api.Application.DTOs;

public sealed class ChannelCreateRequest
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public ChannelType Type { get; set; }
    public bool IsEnabled { get; set; } = true;
}

public sealed class ChannelUpdateRequest
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public ChannelType Type { get; set; }
    public bool IsEnabled { get; set; }
}
