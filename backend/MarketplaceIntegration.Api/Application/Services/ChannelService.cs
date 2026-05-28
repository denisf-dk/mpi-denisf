using MarketplaceIntegration.Api.Application.Abstractions;
using MarketplaceIntegration.Api.Application.DTOs;
using MarketplaceIntegration.Api.Domain.Entities;

namespace MarketplaceIntegration.Api.Application.Services;

public class ChannelService(IChannelRepository repository)
{
    public Task<List<Channel>> GetAllAsync(CancellationToken cancellationToken = default) => repository.GetAllAsync(cancellationToken);

    public Task<Channel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => repository.GetByIdAsync(id, cancellationToken);

    public async Task<Channel> CreateAsync(ChannelCreateRequest request, CancellationToken cancellationToken = default)
    {
        var channel = new Channel
        {
            Name = request.Name,
            Type = request.Type,
            IsEnabled = request.IsEnabled
        };

        await repository.AddAsync(channel, cancellationToken);
        return channel;
    }

    public async Task<bool> UpdateAsync(Guid id, ChannelUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var channel = await repository.GetByIdAsync(id, cancellationToken);
        if (channel is null)
        {
            return false;
        }

        channel.Name = request.Name;
        channel.Type = request.Type;
        channel.IsEnabled = request.IsEnabled;

        await repository.UpdateAsync(channel, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var channel = await repository.GetByIdAsync(id, cancellationToken);
        if (channel is null)
        {
            return false;
        }

        await repository.DeleteAsync(channel, cancellationToken);
        return true;
    }
}
