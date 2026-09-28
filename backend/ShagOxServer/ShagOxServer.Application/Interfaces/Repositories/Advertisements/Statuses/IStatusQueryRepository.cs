using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
public interface IStatusQueryRepository
    : ITranslatableQueryRepository<Status,
        StatusSearchFilter>
{
}