using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserSessions;
public class UserSessionCommandsRepository
    : CommandRepository<UserSession>,
    IUserSessionCommandsRepository
{
    private readonly ICacheInvalidationService<
        UserSession, UserSessionCacheContext> _entityInvalid;

    public UserSessionCommandsRepository(
        AppDbContext db,
        IUserContext userContext,
        ICacheInvalidationService<
        UserSession, UserSessionCacheContext> entityInvalid
    )
        : base(db, userContext)
    {
        _entityInvalid = entityInvalid;
    }

    public async Task<bool> UpdateLastUsedAsync(
        long sessionId,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var query = _db.UserSessions
            .Where(x =>
                x.Id == sessionId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.LastUsedAt, now),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }

    public async Task<bool> RevokeSessionAsync(
        long sessionId,
        SessionRevocationReason reason,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var query = _db.UserSessions
            .Where(x => x.Id == sessionId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.RevokedAt, now)
                    .SetProperty(x => x.RevocationReason, reason),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }
}
