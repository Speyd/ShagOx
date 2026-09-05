using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
public interface ICityTranslationExistsRepository
     : IExistsTranslationRepository<CityTranslation>
{
    Task<bool> ExistsByNameAsync(
        string name);
}