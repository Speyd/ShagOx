using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Deliveries;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Mapping;
public static class DeliveryProviderCacheMapper
{
    public static DeliveryProviderCacheInfo ToInfo(
        DeliveryProvider x,
        EntityStatus status = EntityStatus.Active)
    {
        return new DeliveryProviderCacheInfo
        (
            x.Id,
            status
        );
    }
}
