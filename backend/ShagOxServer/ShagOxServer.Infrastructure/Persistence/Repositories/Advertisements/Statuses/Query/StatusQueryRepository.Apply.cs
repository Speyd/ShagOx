using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Query;
public partial class StatusQueryRepository
    : SearchRepository<Status, StatusSearchFilter>,
      IStatusQueryRepository
{
    protected override IQueryable<Status> ApplyFilter(
       IQueryable<Status> query,
       StatusSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
