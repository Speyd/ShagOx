using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Specification.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
public interface IConditionTranslationExistsRepository
     : ITranslationExistsRepository<ConditionTranslation>
{
    Task<bool> ExistsByNameAsync(
        string name);
}