using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Attributes;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Mapping;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;
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


    private readonly IAttributeDefinitionTranslationQueryRepository _translationRepository;


    public AttributeDefinitionQueryService(
        IAttributeDefinitionQueryRepository attributeQueryRepository,
        IAttributeDefinitionTranslationQueryRepository translationRepository,
        ILanguageProvider language,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(attributeQueryRepository, language, cacheService, settings)
    {
        _attributeRepository = attributeQueryRepository;
        _translationRepository = translationRepository;
    }


    public override async Task<AttributeDefinitionDto> ApplyMapperAsync(
        AttributeDefinition entity)
    {
        var translation = await _translationRepository
            .GetByIdentificatorAsync(entity.Key, _language.Language);

        return AttributeDefinitionMapper.ToDto(entity, translation?.Name);
    }

    public async Task<Result<List<AttributeDefinitionDto>>>
       GetByCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = AttributeDefinitionCache.ValueByCategory(
           categoryId,
           _language.Language,
           status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var values = await _attributeRepository
                    .GetByCategoryAsync(categoryId,
                        cancellationToken, status);

                return await values.ToResultListAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
}
