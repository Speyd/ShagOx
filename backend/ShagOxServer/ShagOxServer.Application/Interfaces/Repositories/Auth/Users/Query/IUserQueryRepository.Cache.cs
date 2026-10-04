using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Users;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
public partial interface IUserQueryRepository
    : IQueryRepository<User, UserSearchFilter>
{
    Task<List<UserCacheInfo>> GetCacheInfosByCityAsync(
        long cityId);
}