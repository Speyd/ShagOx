namespace ShagOxServer.Application.Interfaces.Repositories.Devices;
public interface IDeviceCommandsRepository
{
    Task<bool> BanDeviceByIpAddressAsync(
       string ipAddress,
       DateTime? blockedUntil,
       CancellationToken cancellationToken = default);
    Task<bool> BanDeviceAsync(
        long deviceId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken = default);

    Task<bool> UnbanDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken = default);

    Task UnbanExpiredDevicesAsync(
        CancellationToken cancellationToken = default);

    Task<bool> UpdateLastSeenAsync(
        long deviceId,
        CancellationToken cancellationToken = default);
}
