using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Deliveries;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Delete;
public interface IDeliveryProviderDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<DeliveryProvider, DeleteResponse>
{
}
