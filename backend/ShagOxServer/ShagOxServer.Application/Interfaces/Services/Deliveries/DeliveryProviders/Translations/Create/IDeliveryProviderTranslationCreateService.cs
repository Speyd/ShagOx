using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Base.Create;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Create;
public interface IDeliveryProviderTranslationCreateService
    : ICreateService<
        CreateResponse,
        DeliveryProviderTranslationCreateRequest
        >
{
}
