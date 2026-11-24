namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
public interface IUserDeviceCommandRepository
{
    Task<bool> MarkAsTrasteAsync(
        long deviceId,
        CancellationToken cancellationToken = default);

    Task<bool> UnmarkAsTrasteAsync(
        long deviceId,
        CancellationToken cancellationToken = default);
}
