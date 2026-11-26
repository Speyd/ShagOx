using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Devices.Management;
public interface IDeviceModerationService
{
    Task<Result<bool>> BanDeviceAsync(
        long deviceId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken);

    Task<Result<bool>> UnbanDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken);
}
