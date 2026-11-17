using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Caches;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query.Catalog;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query.Core;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeProcessor;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping.Catalog;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping.Core;
using ShagOxServer.Application.Services.Base.Localized;
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
    protected readonly IAttributeValueService _attributeValueService;
    protected readonly IPriceConversionService _priceConversion;

    protected readonly ILogger<AdvertisementVariantQueryService> _logger;

    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;


    public AdvertisementVariantQueryService(
        IAdvertisementVariantQueryRepository variantRepository,
        IAttributeValueService attributeValueService,
        IPriceConversionService priceConversion,
        ILogger<AdvertisementVariantQueryService> logger,
        ILanguageProvider language,
        IDataCacheService cacheService,
        IOptions<CacheOptions> settings
    )
        : base(variantRepository, language, cacheService, settings)
    {
        _attributeValueService = attributeValueService;
        _priceConversion = priceConversion;
        _logger = logger;
    }


    public override async Task<AdvertisementVariantDto> ApplyMapperAsync(
        AdvertisementVariant entity)
    {
        var attributes = await GetAttributesAsync(entity);

        var convertPrices = await _priceConversion
             .ConvertPricesAsync(
                entity.Price,
                entity.PreviousPrice,
                entity.Advertisement.Currency);

        return AdvertisementVariantMapper.ToDto(
            entity,
            attributes,
            convertPrices);
    }

    public async Task<AdvertisementVariantCatalogDto> ApplyCatalogMapperAsync(
        AdvertisementVariant entity)
    {
        var attributes = await GetAttributesAsync(entity);

        return AdvertisementVariantCatalogMapper.ToDto(
            entity,
            attributes);
    }

    private async Task<List<AttributeDto>> GetAttributesAsync(
        AdvertisementVariant entity)
    {
        var result = await _attributeValueService.GetAttributesAsync(
            entity.Attributes,
            entity.Advertisement.CategoryId);

        if (!result.IsSuccess)
        {
            throw new InvalidOperationException(result.Error);
        }

        return result.Value!;
    }
}
