using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Query;
public partial class UserQueryRepository
    : SearchRepository<User, UserSearchFilter>,
      IUserQueryRepository
{
    public async Task<List<UserCacheInfo>> GetCacheInfosByCityAsync(
        long cityId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Users
            .WithStatusFilter(status)
            .Where(x => x.CityId == cityId)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }

    public async Task<UserCacheInfo?> GetCacheInfoByIdAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        var user = await _db.Users
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x => x.Id == id);

        return user?.SelectCacheInfo();
    }
}
