using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Trast;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Management.Trast;
public partial class UserTrastManagementService
    : IUserTrastManagementService
{
    public async Task<Result<bool>> RevokeSessionAsync(
        long sessionId,
        SessionRevocationReason reason,
        CancellationToken cancellationToken = default)
    {
        return await RevokeSessionInternalAsync(
            sessionId,
            reason,
            checkOwner: true,
            cancellationToken);
    }

    public async Task<Result<bool>> RevokeSessionByAdminAsync(
        long sessionId,
        SessionRevocationReason reason,
        CancellationToken cancellationToken = default)
    {
        return await RevokeSessionInternalAsync(
            sessionId,
            reason,
            checkOwner: false,
            cancellationToken);
    }

    private async Task<Result<bool>> RevokeSessionInternalAsync(
        long sessionId,
        SessionRevocationReason reason,
        bool checkOwner,
        CancellationToken cancellationToken)
    {
        if (!_userContext.UserId.HasValue)
        {
            return Result<bool>.NotFound(EntityNamesResources.User);
        }

        var userSession = await _sessionQueryService
            .GetByIdAsync(sessionId, cancellationToken);

        if (!userSession.IsSuccess)
        {
            return Result<bool>.Fail(userSession.Error);
        }

        if (checkOwner &&
            userSession.Value!.OwnerId != _userContext.UserId.Value)
        {
            return Result<bool>.NotFound(EntityNamesResources.UserSession);
        }

        await _sessionCommandRepository.RevokeSessionAsync(
            sessionId,
            reason,
            cancellationToken);

        return Result<bool>.Success(true);
    }



    public async Task<Result<bool>> RevokeSessionsByUserAsync(
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

        var userSessions = await _sessionQueryService
            .GetAllByUserAsync(userId, cancellationToken);

        if (!userSessions.IsSuccess)
        {
            return Result<bool>.Fail(
                userSessions.Error);
        }

        foreach (var session in userSessions.Value!)
        {
            if (session.Id == usedSession?.Id)
                continue;

            await _sessionCommandRepository
                .RevokeSessionAsync(
                    session.Id,
                    reason,
                    cancellationToken);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> RevokeSessionsByDeviceAsync(
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

        var userSessions = await _sessionQueryService
            .GetAllByUserDeviceAsync(deviceId, cancellationToken);

        if (!userSessions.IsSuccess)
        {
            return Result<bool>.Fail(
                userSessions.Error);
        }

        foreach (var session in userSessions.Value!)
        {
            if (session.Id == usedSession?.Id)
                continue;

            await _sessionCommandRepository
                .RevokeSessionAsync(
                    session.Id,
                    reason,
                    cancellationToken);
        }

        return Result<bool>.Success(true);
    }
}
