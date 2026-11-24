using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
public interface IUserModerationService
{
    Task<Result<bool>> BanUser(
        long userId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken);

    Task<Result<bool>> UnbanUser(
        long userId,
        CancellationToken cancellationToken);

    Task<Result<bool>> BanDevice(
        long deviceId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken);

    Task<Result<bool>> UnbanDevice(
        long deviceId,
        CancellationToken cancellationToken);
}
