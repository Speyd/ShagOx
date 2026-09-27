using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Translations.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses;
public class StatusQueryRepository
    : QueryRepository<Status, StatusSearchFilter>,
      IStatusQueryRepository
{
    public StatusQueryRepository(AppDbContext db)
        : base(db)
    { }

    protected override IQueryable<Status> ApplyFilter(
       IQueryable<Status> query,
       StatusSearchFilter filter)
    {
        return query.Filter(filter);
    }
}