using ShagOxServer.Application.DTOs.Auth.Users.UserSessions;

namespace ShagOxServer.Application.Interfaces.Providers.Sessions;
public interface ISessionInfoProvider
{
    SessionInfo GetSessionInfo();
}
