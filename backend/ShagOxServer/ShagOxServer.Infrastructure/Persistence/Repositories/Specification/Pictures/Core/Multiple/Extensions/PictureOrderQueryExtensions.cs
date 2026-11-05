using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Base.Pictures;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core.Multiple.Extensions;
public static class PictureOrderQueryExtensions
{
    public static IQueryable<PictureCacheInfo> SelectCacheInfo(
       this IQueryable<BasePicture> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new PictureCacheInfo(
            x.Id,
            x.OwnerId,
            status));
    }
}
