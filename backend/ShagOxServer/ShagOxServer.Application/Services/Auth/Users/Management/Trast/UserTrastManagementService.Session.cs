using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Application.Services.Auth.Users.Management.Trast;
public partial class UserTrastManagementService
    : IUserTrastManagementService
{
    public async Task RevokeSessionsByUserAsync(
        long userId,
        SessionRevocationReason reason,
        bool resetUsedSession = false,
        CancellationToken cancellationToken = default)
    {
        var usedSession = await _sessionProvider
            .GetSessionAsync();

        if (usedSession is not null && resetUsedSession)
        {
            await _sessionCommandRepository
                .RevokeSessionAsync(usedSession.Id, reason);
        }

        var userSessions = await _sessionQueryRepository
            .GetAllByUserAsync(userId, cancellationToken);

        foreach (var session in userSessions)
        {
            if (session.Id == usedSession?.Id)
                continue;

            await _sessionCommandRepository
                .RevokeSessionAsync(
                    session.Id,
                    reason,
                    cancellationToken);
        }
    }

    public async Task RevokeSessionsByDeviceAsync(
        long deviceId,
        SessionRevocationReason reason,
        bool resetUsedSession = false,
        CancellationToken cancellationToken = default)
    {
        var usedSession = await _sessionProvider
            .GetSessionAsync();

        if (usedSession is not null && !resetUsedSession)
        {
            await _sessionCommandRepository.RevokeSessionAsync(
                usedSession.Id,
                reason,
                cancellationToken);
        }

        var userSessions = await _sessionQueryRepository
            .GetAllByUserDeviceAsync(deviceId, cancellationToken);

        foreach (var session in userSessions)
        {
            if (session.Id == usedSession?.Id)
                continue;

            await _sessionCommandRepository
                .RevokeSessionAsync(
                    session.Id,
                    reason,
                    cancellationToken);
        }
    }
}
