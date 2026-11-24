using ShagOxServer.Application.Interfaces.Providers.User;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Api.Providers;
public class SessionProvider
    : ISessionProvider
{
    private readonly IUserSessionQueryRepository _sessionService;
    private readonly IUserContext _userContext;


    public SessionProvider(
        IUserSessionQueryRepository sessionService,
        IUserContext userContext)
    {
        _sessionService = sessionService;
        _userContext = userContext;
    }


    public async Task<UserSession?> GetSessionAsync(
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        if (_userContext.RefreshToken is null)
            return null;

        var result = await _sessionService
            .GetByRefreshTokenHashAsync(_userContext.RefreshToken,
                cancellationToken, status);

        return result;
    }
}
