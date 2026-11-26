using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Management.Trast;
public partial class UserTrastManagementService
    : IUserTrastManagementService
{
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
}
