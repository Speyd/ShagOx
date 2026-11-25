namespace ShagOxServer.Application.Interfaces.Services.Auth.Core;
public interface ILogoutService
{
    Task LogoutAsync(
        string refreshToken,
        CancellationToken cancellationToken);

    Task LogoutAllAsync(
        long userId,
        CancellationToken cancellationToken);

    Task LogoutOtherSessions(
        long userId,
        CancellationToken cancellationToken);
}
