using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Core;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Management;
public class UserModerationService
    : IUserModerationService
{
    private readonly IUserQueryRepository _userRepository;
    private readonly IUserCommandsRepository _userCommandsRepository;

    private readonly IUserDeviceQueryRepository _deviceRepository;
    private readonly IUserDeviceCommandsRepository _deviceCommandsRepository;

    private readonly IUserTrastManagementService _trastManagement;


    public UserModerationService(
        IUserQueryRepository userRepository,
        IUserCommandsRepository userCommandsRepository,
        IUserDeviceQueryRepository deviceRepository,
        IUserDeviceCommandsRepository deviceCommandsRepository,
        IUserTrastManagementService trastManagement)
    {
        _userRepository = userRepository;
        _userCommandsRepository = userCommandsRepository;
        _deviceRepository = deviceRepository;
        _deviceCommandsRepository = deviceCommandsRepository;
        _trastManagement = trastManagement;
    }


    public async Task<Result<bool>> BanUser(
        long userId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(
            userId, 
            cancellationToken);

        if (user is null)
        {
            return Result<bool>.Fail(
                EntityNamesResources.User);
        }

        await _trastManagement.RevokeSessionsByUserAsync(user,
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
        var user = await _userRepository.GetByIdAsync(
            userId,
            cancellationToken);

        if (user is null)
        {
            return Result<bool>.Fail(
                EntityNamesResources.User);
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
        var device = await _deviceRepository.GetByIdAsync(
            deviceId,
            cancellationToken);

        if (device is null)
        {
            return Result<bool>.Fail(
                EntityNamesResources.UserDevice);
        }

        await _trastManagement.RevokeSessionsByDeviceAsync(device,
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
        var device = await _deviceRepository.GetByIdAsync(
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
