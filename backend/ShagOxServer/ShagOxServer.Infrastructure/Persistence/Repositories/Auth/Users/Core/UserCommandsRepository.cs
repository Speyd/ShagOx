using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Core;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Core;
public class UserCommandsRepository
    : CommandRepository<User>,
    IUserCommandsRepository
{
    private readonly ICacheInvalidationService<
        User, UserCacheContext> _entityInvalid;


    public UserCommandsRepository(
        AppDbContext db,
        IUserContext userContext,
        ICacheInvalidationService<
            User, UserCacheContext> entityInvalid
    )
        : base(db, userContext)
    {
        _entityInvalid = entityInvalid;
    }


    public async Task<bool> UpdateLastSeenAsync(
        long userId,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var query = _db.Users
            .Where(x => x.Id == userId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.LastSeenAt, now),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }

    public async Task<bool> BanUserAsync(
        long userId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var query = _db.Users
            .Where(x => x.Id == userId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, UserStatus.Blocked)
                    .SetProperty(x => x.BlockedAt, now)
                    .SetProperty(x => x.BlockedUntil, blockedUntil),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }

    public async Task<bool> UnbanUserAsync(
        long userId,
        CancellationToken cancellationToken = default)
    {
        var query = _db.Users
            .Where(x => x.Id == userId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, UserStatus.Active)
                    .SetProperty(x => x.BlockedAt, (DateTime?)null)
                    .SetProperty(x => x.BlockedUntil, (DateTime?)null),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }

    public async Task UnbanExpiredUsersAsync(
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var query = _db.Users
            .Where(x =>
                x.Status == UserStatus.Blocked &&
                x.BlockedUntil != null &&
                x.BlockedUntil <= now);

        await query.ExecuteUpdatesWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, UserStatus.Active)
                    .SetProperty(x => x.BlockedAt, (DateTime?)null)
                    .SetProperty(x => x.BlockedUntil, (DateTime?)null),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }
}
