using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.Core;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Query;
public partial class UserQueryRepository
    : SearchRepository<User, UserSearchFilter>,
      IUserQueryRepository
{
    public async Task<List<UserCacheContext>> GetCacheInfosByCityAsync(
        long cityId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        return await _db.Users
            .AsNoTracking()
            .WithStatusFilter(status)
            .Where(x => x.CityId == cityId)
            .SelectCacheInfo(status)
            .ToListAsync(cancellationToken);
    }

    public async Task<UserCacheContext?> GetCacheInfoByIdAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var user = await _db.Users
            .AsNoTracking()
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x => x.Id == id);

        return user?.SelectCacheInfo(status);
    }
}
