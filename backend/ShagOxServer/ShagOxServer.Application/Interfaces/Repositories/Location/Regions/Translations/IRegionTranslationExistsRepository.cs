using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
public interface IRegionTranslationExistsRepository
     : IExistsTranslationRepository<RegionTranslation>
{
    Task<bool> ExistsByNameAsync(
        string name);
}