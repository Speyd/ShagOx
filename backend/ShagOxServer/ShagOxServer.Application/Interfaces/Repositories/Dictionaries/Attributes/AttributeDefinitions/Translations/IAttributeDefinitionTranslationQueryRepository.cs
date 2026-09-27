using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
public interface IAttributeDefinitionTranslationQueryRepository
    : IQueryTranslationRepository<AttributeDefinitionTranslation, 
        AttributeDefinitionTranslationSearchFilter>
{
}