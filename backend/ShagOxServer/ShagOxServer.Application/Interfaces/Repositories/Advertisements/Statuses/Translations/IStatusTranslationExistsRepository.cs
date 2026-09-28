using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
public interface IStatusTranslationExistsRepository
     : ITranslationExistsRepository<Status,
         StatusTranslation>
{
}