namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions;
public interface IRefreshTokenService
{
    string Generate();
    string Hash(string token);
}
