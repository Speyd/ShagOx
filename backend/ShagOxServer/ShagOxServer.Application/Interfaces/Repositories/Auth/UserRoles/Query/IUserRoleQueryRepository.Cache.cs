using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Auth;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth;
using ShagOxServer.Domain.Filters.Auth.UserRoles;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles.Query;
public partial interface IUserRoleQueryRepository
    : ISearchRepository<UserRole, UserRoleSearchFilter>
{
    Task<List<RoleCacheContext>> GetRoleCacheInfoByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<List<UserCacheContext>> GetUserCacheInfoByRoleAsync(
        long roleId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
