using MarketplaceIntegration.Api.Domain.Entities;

namespace MarketplaceIntegration.Api.Application.Abstractions;

public interface IChannelRepository
{
    Task<List<Channel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Channel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Channel channel, CancellationToken cancellationToken = default);
    Task UpdateAsync(Channel channel, CancellationToken cancellationToken = default);
    Task DeleteAsync(Channel channel, CancellationToken cancellationToken = default);
}
