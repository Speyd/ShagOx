using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Query;
public interface IAttributeDictionaryValueTranslationQueryService
    : ITranslationQueryService<AttributeDictionaryValueTranslationDto,
        AttributeDictionaryValueTranslation,
        AttributeDictionaryValueTranslationSearchFilter>
{
}