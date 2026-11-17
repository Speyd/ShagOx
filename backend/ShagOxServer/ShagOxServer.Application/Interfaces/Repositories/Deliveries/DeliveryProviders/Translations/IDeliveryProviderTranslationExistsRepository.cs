using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Entities.Deliveries.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders.Translations;
public interface IDeliveryProviderTranslationExistsRepository
     : ITranslationExistsRepository<DeliveryProvider,
         DeliveryProviderTranslation>
{
}
