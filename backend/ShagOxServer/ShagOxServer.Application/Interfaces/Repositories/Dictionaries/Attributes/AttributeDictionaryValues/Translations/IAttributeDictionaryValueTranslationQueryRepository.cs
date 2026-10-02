using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
public interface IAttributeDictionaryValueTranslationQueryRepository
    : ITranslationQueryRepository<AttributeDictionaryValue,
        AttributeDictionaryValueTranslation,
        AttributeDictionaryValueTranslationSearchFilter>
{
}