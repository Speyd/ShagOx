namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Core;
public interface IUserCommandsRepository
{
    Task<bool> BanUserAsync(
        long userId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken);

    Task<bool> UnbanUserAsync(
        long userId,
        CancellationToken cancellationToken);

    Task UnbanExpiredUsersAsync(
        CancellationToken cancellationToken);
}
