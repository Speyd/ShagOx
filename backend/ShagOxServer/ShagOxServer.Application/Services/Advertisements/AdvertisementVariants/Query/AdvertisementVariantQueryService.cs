using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeProcessor;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeValues.Query;
using ShagOxServer.Application.Services.Specification.Currencies.Mapping;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Query;
public partial class AdvertisementVariantQueryService
    : BaseLocalizedQueryService<
        AdvertisementVariantDto,
        AdvertisementVariant,
        AdvertisementVariantSearchFilter
        >,
    IAdvertisementVariantQueryService
{
    protected readonly IAdvertisementVariantQueryRepository _variantRepository;
    protected readonly IAttributeDefinitionQueryRepository _attributeRepository;
    protected readonly IAttributeDictionaryValueQueryRepository _valueRepository;
    protected readonly IAttributeValueService _attributeProcessor;
    protected readonly AttributeValueQueryService _variantAttributeService;

    protected readonly IPriceConversionService _priceConversion;

    protected readonly ILogger<AdvertisementVariantQueryService> _logger;

    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;


    public AdvertisementVariantQueryService(
        IAdvertisementVariantQueryRepository variantRepository,
        IAttributeDefinitionQueryRepository attributeRepository,
        IAttributeDictionaryValueQueryRepository valueRepository,
        IAttributeValueService attributeProcessor,
        AttributeValueQueryService variantAttributeService,
        IPriceConversionService priceConversion,
        ILogger<AdvertisementVariantQueryService> logger,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings,
        ILanguageProvider language
    )
        : base(variantRepository, language, cacheService, settings)
    {
        _variantRepository = variantRepository;
        _attributeRepository = attributeRepository;
        _valueRepository = valueRepository;
        _attributeProcessor = attributeProcessor;
        _variantAttributeService = variantAttributeService;
        _priceConversion = priceConversion;
        _logger = logger;
    }


    public override async Task<AdvertisementVariantDto> ApplyMapperAsync(
        AdvertisementVariant entity)
    {
        var attributesResult = await _attributeProcessor
            .GetAttributesAsync(
                entity.Attributes,
                entity.Advertisement.CategoryId
            );

        if (!attributesResult.IsSuccess)
        {
            throw new InvalidOperationException(attributesResult.Error);
        }

        var convertPrices = await _priceConversion
             .ConvertPricesAsync(
                entity.Price,
                entity.PreviousPrice,
                entity.Advertisement.Currency);

        return AdvertisementVariantMapper.ToDto(
            entity,
            attributesResult.Value!,
            convertPrices);
    }
}
