using ShagOxServer.Application.Interfaces.Providers.User;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Api.Providers;
public class SessionProvider
    : ISessionProvider
{
    private readonly IUserSessionQueryRepository _sessionService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IUserContext _userContext;


    public SessionProvider(
        IUserSessionQueryRepository sessionService,
        IRefreshTokenService refreshTokenService,
        IUserContext userContext)
    {
        _sessionService = sessionService;
        _refreshTokenService = refreshTokenService;
        _userContext = userContext;
    }


    public async Task<UserSession?> GetSessionAsync(
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        if (_userContext.RefreshToken is null)
            return null;

        var tokenHash = _refreshTokenService
            .Hash(_userContext.RefreshToken);

        var result = await _sessionService
            .GetByRefreshTokenHashAsync(tokenHash,
                cancellationToken, status);

        return result;
    }
}
