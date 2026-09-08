using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions.Translations;
public interface IAttributeDefinitionTranslationExistsRepository
     : IExistsTranslationRepository<AttributeDefinitionTranslation>
{
    Task<bool> ExistsByNameAsync(
        string name);
}