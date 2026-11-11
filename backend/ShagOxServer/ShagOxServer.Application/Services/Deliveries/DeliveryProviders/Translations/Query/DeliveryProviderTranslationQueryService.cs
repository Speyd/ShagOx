using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Translations.Query;
using ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Query;
using ShagOxServer.Application.Services.Base.Translations.Query;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Mapping;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Entities.Deliveries.Translations;
using ShagOxServer.Domain.Filters.Deliveries.Translations;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Query;

public class DeliveryProviderTranslationQueryService
    : BaseTranslationQueryService<
        DeliveryProviderTranslationDto,
        DeliveryProvider,
        DeliveryProviderTranslation,
        DeliveryProviderTranslationSearchFilter
        >,
    IDeliveryProviderTranslationQueryService
{
    public DeliveryProviderTranslationQueryService(
        IDeliveryProviderTranslationQueryRepository deliveryRepository,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(deliveryRepository, cacheService, settings)
    {
    }


    public override async Task<DeliveryProviderTranslationDto> ApplyMapperAsync(
        DeliveryProviderTranslation entity)
    {
        return DeliveryProviderTranslationMapper.ToDto(entity);
    }
}
