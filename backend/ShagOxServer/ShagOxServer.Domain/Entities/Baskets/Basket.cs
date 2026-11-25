using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Base.Owner;
using ShagOxServer.Domain.Cache.Baskets;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Domain.Entities.Baskets;
public class Basket
    : OwnedEntity<User>,
    IEntityCache<Basket, BasketCacheContext>
{
    public ICollection<BasketItem> BasketItems { get; set; }
        = [];


    public BasketCacheContext GetCacheContext()
    {
        return new BasketCacheContext(
            Id, OwnerId, EntityStatus);
    }

    public override string ToString()
    {
        return $"OwnerId: {OwnerId}";
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
