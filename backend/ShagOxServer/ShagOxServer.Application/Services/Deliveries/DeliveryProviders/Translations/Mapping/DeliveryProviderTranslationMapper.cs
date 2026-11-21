using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Translations.Query;
using ShagOxServer.Domain.Entities.Deliveries.Translations;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Mapping;
public static class DeliveryProviderTranslationMapper
{
    public static DeliveryProviderTranslationDto ToDto(
        DeliveryProviderTranslation x)
    {
        return new DeliveryProviderTranslationDto
        (
            x.Id,
            x.EntityStatus,
            x.Name
        );
    }
}
