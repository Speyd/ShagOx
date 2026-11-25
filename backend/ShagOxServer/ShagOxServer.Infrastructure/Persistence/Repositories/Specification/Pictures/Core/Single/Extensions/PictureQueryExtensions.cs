using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Cache.Specification;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core.Single.Extensions;
public static class PictureQueryExtensions
{
    public static PictureCacheContext SelectCacheInfo(
        this BasePicture x,
        EntityStatus status = EntityStatus.Active)
    {
        return new PictureCacheContext(
            x.Id,
            x.OwnerId,
            status);
    }
}
