using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Mapping;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Filters.Deliveries.DeliveryProviders;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Query;
public class DeliveryProviderQueryService
    : BaseTranslatableQueryService<
        DeliveryProviderDto,
        DeliveryProvider,
        DeliveryProviderSearchFilter
        >,
    IDeliveryProviderQueryService
{
    //private readonly IStatusTranslationQueryRepository _translationRepository;


    public DeliveryProviderQueryService(
        IDeliveryProviderQueryRepository deliveryRepository,
        //IStatusTranslationQueryRepository translationRepository,
        ILanguageProvider language,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(deliveryRepository, language, cacheService, settings)
    {
        //_translationRepository = translationRepository;
    }


    public override async Task<DeliveryProviderDto> ApplyMapperAsync(
        DeliveryProvider entity)
    {
        //var translation = await _translationRepository
        //    .GetByIdentificatorAsync(entity.Code, _language.Language);

        //return DeliveryProviderMapper.ToDto(entity, translation?.Name);

        return DeliveryProviderMapper.ToDto(entity, null);
    }
}
