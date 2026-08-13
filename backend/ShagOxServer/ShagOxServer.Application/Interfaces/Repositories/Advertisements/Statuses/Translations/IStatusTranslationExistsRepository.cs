using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
public interface IStatusTranslationExistsRepository
     : IExistsRepository<StatusTranslation>
{
    Task<bool> ExistsByCodeAsync(
        string name);

    Task<bool> ExistsByLanguageAsync(
        string language);
}