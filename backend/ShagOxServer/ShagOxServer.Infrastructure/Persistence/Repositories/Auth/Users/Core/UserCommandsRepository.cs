using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Core;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Core;
public class UserCommandsRepository
    : CommandRepository<User>,
    IUserCommandsRepository
{
    public UserCommandsRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }

    public async Task<bool> BanUserAsync(
        long userId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var affected = await _db.Users
            .Where(x => x.Id == userId)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, UserStatus.Blocked)
                    .SetProperty(x => x.BlockedAt, now)
                    .SetProperty(x => x.BlockedUntil, blockedUntil),
                cancellationToken);

        return affected > 0;
    }

    public async Task<bool> UnbanUserAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        var affected = await _db.Users
            .Where(x => x.Id == userId)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, UserStatus.Active)
                    .SetProperty(x => x.BlockedAt, (DateTime?)null)
                    .SetProperty(x => x.BlockedUntil, (DateTime?)null),
                cancellationToken);

        return affected > 0;
    }

    public async Task UnbanExpiredUsersAsync(
    CancellationToken cancellationToken)
    {
        await _db.Users
            .Where(x =>
                x.Status == UserStatus.Blocked &&
                x.BlockedUntil != null &&
                x.BlockedUntil <= DateTime.UtcNow)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, UserStatus.Active)
                    .SetProperty(x => x.BlockedAt, (DateTime?)null)
                    .SetProperty(x => x.BlockedUntil, (DateTime?)null),
                cancellationToken);
    }
}
