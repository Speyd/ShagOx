using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserSessions;
public class UserSessionCommandsRepository
    : CommandRepository<UserSession>,
    IUserSessionCommandsRepository
{
    public UserSessionCommandsRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public async Task<bool> RevokeSessionAsync(
        long sessionId,
        SessionRevocationReason reason,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var affected = await _db.UserSessions
            .Where(x => x.Id == sessionId)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.RevokedAt, now)
                    .SetProperty(x => x.RevocationReason, reason),
                cancellationToken);

        return affected > 0;
    }
}
