using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeProcessor;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Query;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeProcessor;
public partial class AttributeProcessor
    : IAttributeProcessor
{
    protected readonly IAdvertisementVariantQueryRepository _variantRepository;
    protected readonly IAttributeDefinitionQueryRepository _attributeRepository;
    protected readonly IAttributeDictionaryValueQueryRepository _valueRepository;
    protected readonly AttributeQueryService _variantAttributeService;

    protected readonly ILogger<AdvertisementVariantQueryService> _logger;


    public AttributeProcessor(
        IAdvertisementVariantQueryRepository variantRepository,
        IAttributeDefinitionQueryRepository attributeRepository,
        IAttributeDictionaryValueQueryRepository valueRepository,
        AttributeQueryService variantAttributeService,
        ILogger<AdvertisementVariantQueryService> logger,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings,
        ILanguageProvider language
    )
    {
        _variantRepository = variantRepository;
        _attributeRepository = attributeRepository;
        _valueRepository = valueRepository;
        _variantAttributeService = variantAttributeService;
        _logger = logger;
    }

}
