using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Validator;
public class AttributeDictionaryValueTranslationValidator
    : BaseTranslationValidator<AttributeDictionaryValue,
        AttributeDictionaryValueTranslation>
{
    public AttributeDictionaryValueTranslationValidator(
        IQueryRepository<AttributeDictionaryValueTranslation> attributeRepository,
        IAttributeDictionaryValueTranslationExistsRepository attributeExistsRepository
    ) : base(attributeRepository, attributeExistsRepository)
    {
    }
}
