using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Domain.Entities.Specification.Pictures;
public class CategoryImage
    : BasePicture<Category>,
    IEntityCache<CategoryImage, PictureCacheContext>
{
    public PictureCacheContext GetCacheContext()
    {
        return new PictureCacheContext(
            Id, OwnerId, EntityStatus);
    }

    public override string ToString()
    {
        return $"Url: {Url}";
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
