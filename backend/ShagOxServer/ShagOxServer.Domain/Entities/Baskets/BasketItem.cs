using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.LineItems.Core;

namespace ShagOxServer.Domain.Entities.Baskets;
public class BasketItem
    : LineItem<Basket>,
    IEntityCache<BasketItem, LineItemCacheContext>
{
    public LineItemCacheContext GetCacheContext()
    {
        return new LineItemCacheContext(
            Id, 
            ParentId, 
            AdvertisementId,
            AdvertisementVariantId,
            EntityStatus);
    }

    public override string ToString()
    {
        return $"Bask.: {ParentId} | AdvertVariant: {AdvertisementVariantId} | Quant.: {Quantity}";
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
