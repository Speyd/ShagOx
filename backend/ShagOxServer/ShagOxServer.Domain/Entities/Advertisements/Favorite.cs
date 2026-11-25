using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Base.Owner;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Domain.Entities.Advertisements;
public class Favorite 
    : OwnedEntity<User>,
    IEntityCache<Favorite, FavoriteCacheContext>
{
    public long AdvertisementId { get; set; }
    public Advertisement Advertisement { get; set; } = null!;


    public FavoriteCacheContext GetCacheContext()
    {
        return new FavoriteCacheContext(
            Id, OwnerId, EntityStatus);
    }

    public override string ToString()
    {
        return $"{OwnerId}-{AdvertisementId}";
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
