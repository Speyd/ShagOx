using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Validator;
public class StatusTranslationValidator
    : BaseTranslationValidator<Status,
        StatusTranslation>
{
    public StatusTranslationValidator(
        IQueryRepository<StatusTranslation> statusRepository,
        IStatusTranslationExistsRepository statusExistsRepository,
        IDataCacheService cache
    ) 
        : base(statusRepository, statusExistsRepository, cache)
    {
    }
}
