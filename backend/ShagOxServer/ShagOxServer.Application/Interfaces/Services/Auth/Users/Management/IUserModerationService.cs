using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
public interface IUserModerationService
{
    Task<Result<bool>> Ban(
        long userId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken);

    Task<Result<bool>> Unban(
        long userId,
        CancellationToken cancellationToken);
}
