using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
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
    protected readonly IAdvertisementVariantQueryRepository _variantRepository;
    protected readonly IAttributeDefinitionQueryRepository _attributeRepository;
    protected readonly IAttributeDictionaryValueQueryRepository _valueRepository;
    protected readonly VariantAttributeQueryService _variantAttributeService;

    protected readonly ILogger<AdvertisementVariantQueryService> _logger;


    public AdvertisementVariantQueryService(
        IAdvertisementVariantQueryRepository variantRepository,
        IAttributeDefinitionQueryRepository attributeRepository,
        IAttributeDictionaryValueQueryRepository valueRepository,
        VariantAttributeQueryService variantAttributeService,
        ILogger<AdvertisementVariantQueryService> logger,
        ICacheService cacheService,
        IOptions<CacheSettings> settings,
        ILanguageProvider language
    )
        : base(variantRepository, language, cacheService, settings)
    {
        _variantRepository = variantRepository;
        _attributeRepository = attributeRepository;
        _valueRepository = valueRepository;
        _variantAttributeService = variantAttributeService;
        _logger = logger;
    }


    public override async Task<AdvertisementVariantDto> ApplyMapperAsync(
        AdvertisementVariant entity)
    {
        var attributesResult = await GetVariantAttributeAsync(entity);

        if (!attributesResult.IsSuccess)
        {
            throw new InvalidOperationException(attributesResult.Error);
        }

        return AdvertisementVariantMapper.ToDto(
            entity,
            attributesResult.Value!);
    }
}