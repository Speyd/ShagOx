using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Avatars;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Avatars;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Query;

public partial class AvatarQueryRepository
    : SearchRepository<Avatar, AvatarSearchFilter>,
      IAvatarQueryRepository
{
    public async Task<PictureCacheInfo?> GetCacheInfoByUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default)
    {
        var avatar = await _db.Avatars
           .FirstOrDefaultAsync(x => x.OwnerId == userId,
                cancellationToken);

        return avatar == null ?
            null :
            new PictureCacheInfo(avatar.Id,
                avatar.OwnerId);
    }
}
