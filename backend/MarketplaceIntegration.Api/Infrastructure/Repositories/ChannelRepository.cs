using MarketplaceIntegration.Api.Application.Abstractions;
using MarketplaceIntegration.Api.Domain.Entities;
using MarketplaceIntegration.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceIntegration.Api.Infrastructure.Repositories;

public class ChannelRepository(MarketplaceDbContext context) : IChannelRepository
{
    public Task<List<Channel>> GetAllAsync(CancellationToken cancellationToken = default)
        => context.Channels.AsNoTracking().ToListAsync(cancellationToken);

    public Task<Channel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => context.Channels.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddAsync(Channel channel, CancellationToken cancellationToken = default)
    {
        context.Channels.Add(channel);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Channel channel, CancellationToken cancellationToken = default)
    {
        context.Channels.Update(channel);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Channel channel, CancellationToken cancellationToken = default)
    {
        context.Channels.Remove(channel);
        await context.SaveChangesAsync(cancellationToken);
    }
}
