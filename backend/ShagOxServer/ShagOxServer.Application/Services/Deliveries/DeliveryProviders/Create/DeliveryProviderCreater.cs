using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Create;
using ShagOxServer.Domain.Entities.Deliveries;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Create;
public static class DeliveryProviderCreater
{
    public static DeliveryProvider Create(
       DeliveryProviderCreateRequest request)
    {
        return new DeliveryProvider
        {
            Code = request.Code
        };
    }
}
