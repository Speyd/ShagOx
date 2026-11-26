using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Moderation;
public partial interface IUserModerationService
{
    Task<Result<bool>> BanUserAsync(
        long userId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken);

    Task<Result<bool>> UnbanUserAsync(
        long userId,
        CancellationToken cancellationToken);
}
