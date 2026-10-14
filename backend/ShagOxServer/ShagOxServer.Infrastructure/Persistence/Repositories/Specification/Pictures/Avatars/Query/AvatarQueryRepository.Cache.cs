using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Avatars;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Avatars;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Query;

public partial class AvatarQueryRepository
    : SearchRepository<Avatar, AvatarSearchFilter>,
      IAvatarQueryRepository
{
    public async Task<AvatarCacheInfo?> GetCacheInfoByUserIdAsync(
        long userId)
    {
        var avatar = await _db.Avatars
           .FirstOrDefaultAsync(x => x.UserId == userId);

        return avatar == null ?
            null :
            new AvatarCacheInfo(avatar.Id,
                avatar.UserId);
    }
}
