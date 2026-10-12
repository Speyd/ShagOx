using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Validator;
public class AttributeDefinitionTranslationValidator
    : BaseTranslationValidator<AttributeDefinition,
        AttributeDefinitionTranslation>
{
    public AttributeDefinitionTranslationValidator(
        IQueryRepository<AttributeDefinitionTranslation> attributeRepository,
        IAttributeDefinitionTranslationExistsRepository attributeExistsRepository
    ) : base(attributeRepository, attributeExistsRepository)
    {
    }
}
