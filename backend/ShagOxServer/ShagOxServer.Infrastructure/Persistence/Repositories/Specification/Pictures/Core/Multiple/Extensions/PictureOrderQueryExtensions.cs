using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Cache.Specification;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core.Multiple.Extensions;
public static class PictureOrderQueryExtensions
{
    public static IQueryable<PictureCacheContext> SelectCacheInfo(
       this IQueryable<BasePicture> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new PictureCacheContext(
            x.Id,
            x.OwnerId,
            status));
    }
}
