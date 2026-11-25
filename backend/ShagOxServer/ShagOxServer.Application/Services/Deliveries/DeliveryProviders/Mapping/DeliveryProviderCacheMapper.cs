using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Deliveries;
using ShagOxServer.Domain.Entities.Deliveries;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Mapping;
public static class DeliveryProviderCacheMapper
{
    public static DeliveryProviderCacheContext ToInfo(
        DeliveryProvider x,
        EntityStatus status = EntityStatus.Active)
    {
        return new DeliveryProviderCacheContext
        (
            x.Id,
            status
        );
    }
}
