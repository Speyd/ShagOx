using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Domain.Entities.Specification.Pictures;
public class Avatar 
    : BasePicture<User>,
    IEntityCache<Avatar, PictureCacheContext>
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
