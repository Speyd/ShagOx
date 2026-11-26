using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Moderation;
public partial interface IUserModerationService
{
    Task<Result<bool>> BanUserDeviceAsync(
        long deviceId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken);

    Task<Result<bool>> UnbanUserDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken);
}
