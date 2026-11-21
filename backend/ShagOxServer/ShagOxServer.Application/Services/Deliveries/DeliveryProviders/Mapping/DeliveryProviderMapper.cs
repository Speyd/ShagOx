using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Domain.Entities.Deliveries;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Mapping;
public static class DeliveryProviderMapper
{
    public static DeliveryProviderDto ToDto(
        DeliveryProvider x,
        string? label)
    {
        return new DeliveryProviderDto
        (
            x.Id,
            x.EntityStatus,
            x.Code,
            label
        );
    }
}
