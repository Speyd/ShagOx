using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
public interface IStatusQueryRepository
    : ISearchTranslatableRepository<Status,
        StatusSearchFilter>
{
}
