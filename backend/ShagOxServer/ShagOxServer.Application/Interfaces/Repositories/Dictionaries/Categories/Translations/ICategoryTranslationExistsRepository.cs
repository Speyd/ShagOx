using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
public interface ICategoryTranslationExistsRepository
     : ITranslationExistsRepository<CategoryTranslation>
{
    Task<bool> ExistsByNameAsync(
        string name);
}