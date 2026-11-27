using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserSessions;
public class UserSessionExistsRepository
    : ExistsOwnerRepository<UserSession>,
      IUserSessionExistsRepository
{
    public UserSessionExistsRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }

    public async Task<bool> IsRevokedAsync(
        int userId,
        string refreshToken,
        CancellationToken cancellationToken)
    {
        return await _db.UserSessions
            .AsNoTracking()
            .AnyAsync(
                s => s.OwnerId == userId &&
                     s.RefreshTokenHash == refreshToken &&
                     s.RevokedAt != null,
                cancellationToken);
    }

    public Task<bool> IsActiveAsync(
        long sessionId,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        return _db.UserSessions
            .AnyAsync(
                x => x.Id == sessionId &&
                     x.RevokedAt == null &&
                     x.ExpiresAt > now,
                cancellationToken);
    }
}
