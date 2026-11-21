using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Deliveries.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Delete;
public interface IDeliveryProviderTranslationDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<DeliveryProviderTranslation, DeleteResponse>
{
}
