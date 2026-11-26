using ShagOxServer.Application.Interfaces.Providers.User;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Management;
public class UserTrastManagementService
    : IUserTrastManagementService
{
    private readonly IUserContext _userContext;
    private readonly IUserQueryService _userService;

    private readonly IDeviceProvider _deviceProvider;
    private readonly IUserDeviceCommandsRepository _deviceCommandRepository;
    private readonly IUserDeviceQueryService _deviceQueryService;

    private readonly ISessionProvider _sessionProvider;
    private readonly IUserSessionCommandsRepository _sessionCommandRepository;
    private readonly IUserSessionQueryRepository _sessionQueryRepository;


    public UserTrastManagementService(
        IUserContext userContext,
        IUserQueryService userService,
        IDeviceProvider deviceProvider,
        IUserDeviceCommandsRepository deviceCommandRepository,
        IUserDeviceQueryService deviceQueryService,
        ISessionProvider sessionProvider,
        IUserSessionCommandsRepository sessionCommandRepository,
        IUserSessionQueryRepository sessionQueryRepository)
    {
        _userContext = userContext;
        _userService = userService;
        _deviceProvider = deviceProvider;
        _deviceCommandRepository = deviceCommandRepository;
        _deviceQueryService = deviceQueryService;
        _sessionProvider = sessionProvider;
        _sessionCommandRepository = sessionCommandRepository;
        _sessionQueryRepository = sessionQueryRepository;
    }

    public async Task<Result<bool>> UntrustDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken)
    {
        if (_userContext.UserId is null)
        {
            return Result<bool>.NotFound(
                EntityNamesResources.User);
        }

        var userDto = await _userService.GetByIdAsync(
            _userContext.UserId.Value,
            cancellationToken);

        if (!userDto.IsSuccess)
        {
            return Result<bool>.NotFound(
                EntityNamesResources.User);
        }

        var usedDevice = await _deviceQueryService
            .GetByIdAsync(
                deviceId,
                cancellationToken);

        if (!usedDevice.IsSuccess || 
            userDto.Value!.Id == usedDevice.Value!.OwnerId)
        {
            return Result<bool>.NotFound(usedDevice.Error ??
                EntityNamesResources.UserDevice);
        }

        await _deviceCommandRepository
            .UnmarkAsTrasteAsync(
                usedDevice.Value!.Id,
                cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> UntrustDevicesAsync(
        User user,
        bool resetUsedDevice = false,
        CancellationToken cancellationToken = default)
    {
        var usedDevice = await _deviceProvider
            .GetDeviceAsync();

        if (usedDevice is not null && resetUsedDevice)
        {
            await _deviceCommandRepository
                .UnmarkAsTrasteAsync(
                    usedDevice.Id,
                    cancellationToken);
        }

        var userDevices = await _deviceQueryService
            .GetAllByUserAsync(user.Id, cancellationToken);

        if (!userDevices.IsSuccess || 
            userDevices.Value is null)
        {
            return Result<bool>.Fail(
                userDevices.Error ?? EntityNamesResources.UserDevice);
        }

        foreach (var device in userDevices.Value)
        {
            if (device.Id == usedDevice?.Id)
                continue;

            await _deviceCommandRepository
                .UnmarkAsTrasteAsync(
                    device.Id,
                    cancellationToken);
        }

        return Result<bool>.Success(true);
    }

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
            .GetAllByDeviceAsync(deviceId, cancellationToken);

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
