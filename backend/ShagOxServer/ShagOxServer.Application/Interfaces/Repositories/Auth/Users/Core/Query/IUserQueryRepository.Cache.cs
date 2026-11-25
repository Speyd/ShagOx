using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.Core;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
public partial interface IUserQueryRepository
    : ISearchRepository<User, UserSearchFilter>
{
    Task<List<UserCacheContext>> GetCacheInfosByCityAsync(
        long cityId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<UserCacheContext?> GetCacheInfoByIdAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
