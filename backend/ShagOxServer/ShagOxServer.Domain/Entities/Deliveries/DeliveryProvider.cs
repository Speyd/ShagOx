using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Deliveries;
using ShagOxServer.Domain.Entities.Deliveries.Translations;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Domain.Entities.Deliveries;
public class DeliveryProvider
     : BaseTranslatable,
    IEntityCache<DeliveryProvider, DeliveryProviderCacheContext>
{
    public string Code { get; set; } = null!;

    public ICollection<DeliveryProviderTranslation> Translations { get; set; }
        = [];

    public ICollection<Order> Orders { get; set; }
        = [];

    public DeliveryProviderCacheContext GetCacheContext()
    {
        return new DeliveryProviderCacheContext(
            Id, EntityStatus);
    }

    public override string GetIdentificator()
    {
        return Code;
    }

    public override string ToString()
    {
        return $"Id: {Id} | Code: {Code}";
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
