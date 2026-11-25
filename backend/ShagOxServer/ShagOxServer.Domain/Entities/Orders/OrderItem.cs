using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.LineItems.Core;

namespace ShagOxServer.Domain.Entities.Orders;
public class OrderItem
    : LineItem<Order>,
    IEntityCache<OrderItem, LineItemCacheContext>
{
    public decimal TotalWeight { get; set; }
    public decimal TotalPrice { get; set; }

    public decimal Price => Quantity > 0
        ? TotalPrice / Quantity
        : 0m;


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
        return Id.ToString();
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
