using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Domain.Entities.Specification.Pictures;
public class AdvertisementImage 
    : BasePictureOrder<Advertisement>,
    IEntityCache<AdvertisementImage, PictureCacheContext>
{
    public PictureCacheContext GetCacheContext()
    {
        return new PictureCacheContext(
            Id, OwnerId, EntityStatus);
    }

    public override string ToString()
    {
        return $"Order: {Order} | Url: {Url}";
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
