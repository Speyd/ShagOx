using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
public interface IStatusTranslationExistsRepository
     : IExistsTranslationRepository<StatusTranslation>
{
    Task<bool> ExistsByNameAsync(
        string name);
}