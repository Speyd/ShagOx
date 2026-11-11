using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Entities.Deliveries.Translations;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Validator;
public class DeliveryProviderTranslationValidator
    : BaseTranslationValidator<DeliveryProvider,
        DeliveryProviderTranslation>
{
    public DeliveryProviderTranslationValidator(
        IRepository<DeliveryProviderTranslation> repository,
        IQueryRepository<DeliveryProviderTranslation> deliveryRepository,
        IDeliveryProviderTranslationExistsRepository deliveryExistsRepository,
        IDataCacheService cache
    )
        : base(repository, deliveryRepository, deliveryExistsRepository, cache)
    {
    }
}
