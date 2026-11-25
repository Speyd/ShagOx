using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
public interface IUserSessionCommandsRepository
{
    Task<bool> RevokeSessionAsync(
        long sessionId,
        SessionRevocationReason reason,
        CancellationToken cancellationToken = default);
}
