using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
public interface IProductTypeTranslationExistsRepository
     : ITranslationExistsRepository<ProductTypeTranslation>
{
    Task<bool> ExistsByNameAsync(
        string name);
}