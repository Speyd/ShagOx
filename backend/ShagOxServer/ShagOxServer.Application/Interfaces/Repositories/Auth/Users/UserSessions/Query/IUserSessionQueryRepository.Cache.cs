using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserSessions;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions.Query;
public partial interface IUserSessionQueryRepository
    : ISearchRepository<UserSession, UserSessionSearchFilter>
{
    Task<List<UserSessionCacheContext>> GetCacheContextByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<List<UserSessionCacheContext>> GetCacheContextByUserDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<List<UserSessionCacheContext>> GetCacheContextByIpAddressAsync(
        string ipAddress,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
