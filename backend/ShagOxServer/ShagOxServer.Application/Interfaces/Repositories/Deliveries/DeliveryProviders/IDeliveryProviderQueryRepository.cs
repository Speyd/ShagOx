using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Entities.Deliveries;

namespace ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders;
public interface IDeliveryProviderQueryRepository
    : ISearchTranslatableRepository<DeliveryProvider,
        DeliveryProviderSearchFilter>
{
}
