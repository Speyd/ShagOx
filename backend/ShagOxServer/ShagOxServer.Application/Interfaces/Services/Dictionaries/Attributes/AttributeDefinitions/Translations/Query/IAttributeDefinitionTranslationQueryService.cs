using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Query;
public interface IAttributeDefinitionTranslationQueryService
    : ITranslationQueryService<AttributeDefinitionTranslationDto,
        AttributeDefinitionTranslation,
        AttributeDefinitionTranslationSearchFilter>
{
}