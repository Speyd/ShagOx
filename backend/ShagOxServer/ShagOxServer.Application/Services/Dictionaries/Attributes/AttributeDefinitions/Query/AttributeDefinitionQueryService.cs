using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Attributes;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
public class AttributeDefinitionQueryService 
    : BaseTranslatableQueryService<
        AttributeDefinitionDto,
        AttributeDefinition,
        AttributeDefinitionSearchFilter
        >,
    IAttributeDefinitionQueryService
{
    public readonly IAttributeDefinitionQueryRepository _attributeRepository;
    public readonly IAttributeDictionaryValueQueryRepository _dictQueryRepository;
    public readonly IAttributeDictionaryValueQueryService _dictQueryService;


    private readonly IAttributeDefinitionTranslationQueryRepository _translationRepository;


    public AttributeDefinitionQueryService(
        IAttributeDefinitionQueryRepository attributeQueryRepository,
        IAttributeDefinitionTranslationQueryRepository translationRepository,
        IAttributeDictionaryValueQueryRepository dictQueryRepository,
        IAttributeDictionaryValueQueryService dictQueryService,
        ILanguageProvider language,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(attributeQueryRepository, language, cacheService, settings)
    {
        _attributeRepository = attributeQueryRepository;
        _translationRepository = translationRepository;
        _dictQueryRepository = dictQueryRepository;
        _dictQueryService = dictQueryService;
    }


    public override async Task<AttributeDefinitionDto> ApplyMapperAsync(
        AttributeDefinition entity)
    {
        var translation = await _translationRepository
            .GetByIdentificatorAsync(entity.Key, _language.Language);

        return AttributeDefinitionMapper.ToDto(entity, translation?.Name);
    }

    public async Task<Result<PagedResult<AttributeDictionaryValueDto>>>
       GetValuesByCaregoryAsync(
           long categoryId,
           PaginationParams pagination,
           CancellationToken cancellationToken = default)
    {
        var cacheKey = AttributeDefinitionCache.ValueByCategory(
           categoryId,
           _language.Language,
           pagination.Page,
           pagination.PageSize);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var attributreDefinition = await _attributeRepository
                    .GetByCategoryAsync(categoryId, cancellationToken);

                var dictionaryIds = attributreDefinition
                    .Select(x => x.DictionaryId)
                    .ToList();

                var values = await _dictQueryRepository
                    .GetByDictionariesAsync(dictionaryIds, pagination,
                        cancellationToken);

                return await values.ToResultPagedAsync(
                    _dictQueryService.ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
}
