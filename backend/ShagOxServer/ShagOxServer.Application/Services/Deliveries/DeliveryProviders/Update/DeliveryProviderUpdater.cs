using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Update;
using ShagOxServer.Domain.Entities.Deliveries;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Update;
public class DeliveryProviderUpdater
{
    public static int ApplyUpdates(
        DeliveryProvider delivery,
        DeliveryProviderUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Code is not null)
        {
            delivery.Code = request.Code;
            countUpdated++;
        }

        return countUpdated;
    }
}
