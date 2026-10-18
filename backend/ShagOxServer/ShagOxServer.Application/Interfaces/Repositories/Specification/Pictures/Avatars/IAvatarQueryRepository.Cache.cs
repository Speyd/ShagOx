using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Avatars;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Avatars;
public partial interface IAvatarQueryRepository
    : ISearchRepository<Avatar, AvatarSearchFilter>
{
    Task<PictureCacheInfo?> GetCacheInfoByUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default);
}
