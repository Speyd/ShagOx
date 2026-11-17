using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Entities.Deliveries.Translations;
using ShagOxServer.Domain.Filters.Deliveries.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders.Translations;
public interface IDeliveryProviderTranslationQueryRepository
    : ISearchTranslationRepository<DeliveryProvider,
        DeliveryProviderTranslation,
        DeliveryProviderTranslationSearchFilter>
{
}
