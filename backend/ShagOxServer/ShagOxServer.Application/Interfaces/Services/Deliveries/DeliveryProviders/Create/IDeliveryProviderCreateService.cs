using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Create;
using ShagOxServer.Application.Interfaces.Services.Base.Create;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Create;
public interface IDeliveryProviderCreateService
    : ICreateService<
        CreateResponse,
        DeliveryProviderCreateRequest
        >
{
}
