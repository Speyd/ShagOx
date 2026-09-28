using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Query;
public class AttributeDictionaryValueTranslationQueryService
    : BaseTranslationQueryService<
        AttributeDictionaryValueTranslationDto,
        AttributeDictionaryValue,
        AttributeDictionaryValueTranslation,
        AttributeDictionaryValueTranslationSearchFilter
        >,
    IAttributeDictionaryValueTranslationQueryService
{
    public AttributeDictionaryValueTranslationQueryService(
        IAttributeDictionaryValueTranslationQueryRepository attributeRepository
    )
        : base(attributeRepository)
    {
    }


    public override async Task<AttributeDictionaryValueTranslationDto>
        ApplyMapperAsync(
        AttributeDictionaryValueTranslation entity)
    {
        return AttributeDictionaryValueTranslationMapper
            .ToDto(entity);
    }
}