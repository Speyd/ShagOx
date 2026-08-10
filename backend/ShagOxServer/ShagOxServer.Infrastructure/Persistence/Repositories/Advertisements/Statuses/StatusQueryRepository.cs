using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses;
public class StatusQueryRepository
    : QueryRepository<Status>,
      IStatusQueryRepository
{
    public StatusQueryRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<PagedResult<Status>> Search(
        StatusSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.Statuses
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}