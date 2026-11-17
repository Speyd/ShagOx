using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Translations.Translation;
using ShagOxServer.Domain.Entities.Deliveries.Translations;
using ShagOxServer.Domain.Filters.Deliveries.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Query;
public interface IDeliveryProviderTranslationQueryService
    : ITranslationQueryService<DeliveryProviderTranslationDto,
        DeliveryProviderTranslation,
        DeliveryProviderTranslationSearchFilter>
{
}
