namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
public interface IUserDeviceCommandsRepository
{
    Task<bool> MarkAsTrasteAsync(
        long deviceId,
        CancellationToken cancellationToken = default);

    Task<bool> UnmarkAsTrasteAsync(
        long deviceId,
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

    Task<bool> BanDeviceByIpAsync(
        string ip,
        DateTime? blockedUntil,
        CancellationToken cancellationToken = default);

    Task<bool> UnbanDeviceByIpAsync(
        string ip,
        CancellationToken cancellationToken = default);
}
