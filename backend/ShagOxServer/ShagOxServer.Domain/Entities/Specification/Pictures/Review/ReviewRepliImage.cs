using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Comments.Review;

namespace ShagOxServer.Domain.Entities.Specification.Pictures.Review;
public class ReviewRepliImage
    : BasePictureOrder<ReviewRepliComment>,
    IEntityCache<ReviewRepliImage, PictureCacheContext>
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
