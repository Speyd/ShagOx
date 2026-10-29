using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeProcessor;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeValues.Query;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeProcessor;
public partial class AttributeValueService
    : IAttributeValueService
{
    protected readonly IAttributeDefinitionQueryRepository _attributeRepository;
    protected readonly IAttributeDefinitionQueryService _attributeService;
    protected readonly IAttributeDictionaryValueQueryRepository _valueRepository;
    protected readonly IAttributeDictionaryValueQueryService _valueService;

    protected readonly ILogger<AdvertisementVariantQueryService> _logger;


    public AttributeValueService(
        IAttributeDefinitionQueryRepository attributeRepository,
        IAttributeDefinitionQueryService attributeService,
        IAttributeDictionaryValueQueryRepository valueRepository,
        IAttributeDictionaryValueQueryService valueService,
        AttributeValueQueryService variantAttributeService,
        ILogger<AdvertisementVariantQueryService> logger,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings,
        ILanguageProvider language
    )
    {
        _attributeRepository = attributeRepository;
        _attributeService = attributeService;
        _valueRepository = valueRepository;
        _valueService = valueService;
        _logger = logger;
    }

}
