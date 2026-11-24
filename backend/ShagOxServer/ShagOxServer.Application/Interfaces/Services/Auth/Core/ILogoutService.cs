namespace ShagOxServer.Application.Interfaces.Services.Auth.Core;
public interface ILogoutService
{
    Task LogoutAsync(
        string refreshToken,
        CancellationToken cancellationToken);
}
