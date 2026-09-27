using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
public interface IAttributeDefinitionTranslationExistsRepository
     : ITranslationExistsRepository<AttributeDefinitionTranslation>
{
    Task<bool> ExistsByNameAsync(
        string name);
}