using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Entities.Orders;
using System.Text.Json;

namespace ShagOxServer.Domain.Entities.Advertisements;
public class AdvertisementVariant
    : BaseEntity,
    IEntityCache<AdvertisementVariant, AdvertisementVariantCacheContext>
{
    public long AdvertisementId { get; set; }
    public Advertisement Advertisement { get; set; } = null!;

    /// <summary>
    /// Current product price in the specified currency.
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Previous product price used for displaying discounts and price changes.
    /// </summary>
    public decimal? PreviousPrice { get; set; }

    public int Stock { get; set; }

    public decimal Weight { get; set; }

    public JsonDocument Attributes { get; set; } = null!;

    public ICollection<BasketItem> BasketItems { get; set; }
        = [];

    public ICollection<OrderItem> OrderItems { get; set; }
        = [];


    public AdvertisementVariantCacheContext GetCacheContext()
    {
        return new AdvertisementVariantCacheContext(
            Id,
            EntityStatus);
    }

    public override string ToString()
    {
        return $"Price: {Price} | Stock: {Stock}";
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
