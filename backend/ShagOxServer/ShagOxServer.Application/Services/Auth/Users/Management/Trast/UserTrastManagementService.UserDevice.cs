using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Trast;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Management.Trast;
public partial class UserTrastManagementService
    : IUserTrastManagementService
{
    public async Task<Result<bool>> UntrustDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken = default)
    {
        return await UntrustDeviceInternalAsync(
            deviceId,
            checkOwner: true,
            cancellationToken);
    }

    public async Task<Result<bool>> UntrustDeviceByAdminAsync(
        long deviceId,
        CancellationToken cancellationToken = default)
    {
        return await UntrustDeviceInternalAsync(
            deviceId,
            checkOwner: false,
            cancellationToken);
    }

    private async Task<Result<bool>> UntrustDeviceInternalAsync(
        long deviceId,
        bool checkOwner,
        CancellationToken cancellationToken)
    {
        if (checkOwner && !_userContext.UserId.HasValue)
        {
            return Result<bool>.NotFound(
                EntityNamesResources.User);
        }
 
        var usedDevice = await _deviceQueryService
            .GetByIdAsync(deviceId, cancellationToken);

        if (!usedDevice.IsSuccess)
        {
            return Result<bool>.Fail(usedDevice.Error);
        }

        if (checkOwner &&
            _userContext.UserId!.Value != usedDevice.Value!.OwnerId)
        {
            return Result<bool>.NotFound(
                EntityNamesResources.UserDevice);
        }

        await _deviceCommandRepository.UnmarkAsTrasteAsync(
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
}
