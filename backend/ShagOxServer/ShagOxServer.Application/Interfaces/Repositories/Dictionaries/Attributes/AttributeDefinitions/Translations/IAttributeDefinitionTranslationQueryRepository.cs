using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
public interface IAttributeDefinitionTranslationQueryRepository
    : ISearchTranslationRepository<AttributeDefinition,
        AttributeDefinitionTranslation, 
        AttributeDefinitionTranslationSearchFilter>
{
}
