using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Core;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;
using UAParser;

namespace ShagOxServer.Application.Services.Auth.Users.Management;
public class UserModerationService
    : IUserModerationService
{
    private readonly IUserQueryService _userService;
    private readonly IUserCommandsRepository _userCommandsRepository;

    private readonly IUserDeviceQueryService _deviceService;
    private readonly IUserDeviceCommandsRepository _deviceCommandsRepository;

    private readonly IUserTrastManagementService _trastManagement;


    public UserModerationService(
        IUserQueryService userService,
        IUserCommandsRepository userCommandsRepository,
        IUserDeviceQueryService deviceService,
        IUserDeviceCommandsRepository deviceCommandsRepository,
        IUserTrastManagementService trastManagement)
    {
        _userService = userService;
        _userCommandsRepository = userCommandsRepository;
        _deviceService = deviceService;
        _deviceCommandsRepository = deviceCommandsRepository;
        _trastManagement = trastManagement;
    }


    public async Task<Result<bool>> BanUser(
        long userId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(
            userId, 
            cancellationToken);

        if (!user.IsSuccess ||
            user.Value is null)
        {
            return Result<bool>.Fail(
               user.Error ?? EntityNamesResources.User);
        }

        await _trastManagement.RevokeSessionsByUserAsync(
            user.Value.Id,
            SessionRevocationReason.AccountBlocked,
            true);

        await _userCommandsRepository.BanUserAsync(
            userId,
            blockedUntil,
            cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> UnbanUser(
        long userId,
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(
            userId,
            cancellationToken);

        if (!user.IsSuccess ||
           user.Value is null)
        {
            return Result<bool>.Fail(
               user.Error ?? EntityNamesResources.User);
        }

        await _userCommandsRepository.UnbanUserAsync(
            userId,
            cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> BanDevice(
        long deviceId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken)
    {
        var device = await _deviceService.GetByIdAsync(
            deviceId,
            cancellationToken);

        if (!device.IsSuccess || 
            device.Value is null)
        {
            return Result<bool>.Fail(
                device.Error ?? EntityNamesResources.UserDevice);
        }

        await _trastManagement.RevokeSessionsByDeviceAsync(
            device.Value!.Id,
            SessionRevocationReason.AccountBlocked,
            true);

        await _deviceCommandsRepository.BanDeviceAsync(
            deviceId,
            blockedUntil,
            cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> UnbanDevice(
        long deviceId,
        CancellationToken cancellationToken)
    {
        var device = await _deviceService.GetByIdAsync(
           deviceId,
           cancellationToken);

        if (device is null)
        {
            return Result<bool>.Fail(
                EntityNamesResources.UserDevice);
        }

        await _deviceCommandsRepository.UnbanDeviceAsync(
            deviceId,
            cancellationToken);

        return Result<bool>.Success(true);
    }
}
