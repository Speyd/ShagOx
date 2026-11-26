using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserDevices;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices.Query;
public partial interface IUserDeviceQueryRepository
    : ISearchRepository<UserDevice, UserDeviceSearchFilter>
{
    Task<List<UserDeviceCacheContext>> GetCacheContextByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
