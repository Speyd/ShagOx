using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Query;
using ShagOxServer.Application.Services.Base.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Query;
public class AttributeDefinitionTranslationQueryService
    : BaseTranslationQueryService<
        AttributeDefinitionTranslationDto,
        AttributeDefinition,
        AttributeDefinitionTranslation,
        AttributeDefinitionTranslationSearchFilter
        >,
    IAttributeDefinitionTranslationQueryService
{
    public AttributeDefinitionTranslationQueryService(
        IAttributeDefinitionTranslationQueryRepository attributeRepository,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(attributeRepository, cacheService, settings)
    {
    }


    public override async Task<AttributeDefinitionTranslationDto>
        ApplyMapperAsync(
        AttributeDefinitionTranslation entity)
    {
        return AttributeDefinitionTranslationMapper
            .ToDto(entity);
    }
}