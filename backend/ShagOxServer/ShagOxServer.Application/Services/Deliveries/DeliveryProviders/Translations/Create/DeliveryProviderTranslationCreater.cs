using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Translations.Create;
using ShagOxServer.Domain.Entities.Deliveries.Translations;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Create;
public static class DeliveryProviderTranslationCreater
{
    public static DeliveryProviderTranslation Create(
        DeliveryProviderTranslationCreateRequest request)
    {
        return new DeliveryProviderTranslation
        {
            TranslatableId = request.TranslatableId,
            Language = request.Language,
            Name = request.Name,
        };
    }
}
