using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
public interface IProductTypeTranslationExistsRepository
     : IExistsTranslationRepository<ProductTypeTranslation>
{
    Task<bool> ExistsByNameAsync(
        string name);
}