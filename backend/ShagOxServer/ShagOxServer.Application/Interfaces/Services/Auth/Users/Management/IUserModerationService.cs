using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
public interface IUserModerationService
{
    Task<Result<bool>> BanUserAsync(
        long userId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken);

    Task<Result<bool>> UnbanUserAsync(
        long userId,
        CancellationToken cancellationToken);

    Task<Result<bool>> BanUserDeviceAsync(
        long deviceId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken);

    Task<Result<bool>> UnbanUserDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken);
}
