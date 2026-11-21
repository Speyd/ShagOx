using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Base.Update;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Update;
public interface IDeliveryProviderTranslationUpdateService
    : IUpdateService<UpdateResponse,
        DeliveryProviderTranslationUpdateRequest>,
    IEntityStatusUpdateService
{
}
