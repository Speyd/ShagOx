using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Management.Moderation;
public partial class UserModerationService
    : IUserModerationService
{
    public async Task<Result<bool>> BanUserDeviceAsync(
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

    public async Task<Result<bool>> UnbanUserDeviceAsync(
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
