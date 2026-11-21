using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Update;
using ShagOxServer.Application.Interfaces.Services.Base.Update;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Update;
public interface IDeliveryProviderUpdateService
    : IUpdateService<UpdateResponse, DeliveryProviderUpdateRequest>,
    IEntityStatusUpdateService
{
}
