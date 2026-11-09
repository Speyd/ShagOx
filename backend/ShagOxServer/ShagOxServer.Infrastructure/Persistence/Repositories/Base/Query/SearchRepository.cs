using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
public class SearchRepository<TEntity, TFilter>
    : QueryRepository<TEntity>,
    ISearchRepository<TEntity, TFilter>
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    public SearchRepository(ReplicaDbContext db)
        : base(db)
    {
    }

    
    public virtual async Task<PagedResult<TEntity>> SearchAsync(
        TFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        IQueryable<TEntity> query = _db.Set<TEntity>();

        query = ApplyIncludes(query);
        query = ApplyFilter(query, filter);

        return await query
            .AsNoTracking()
            .WithStatusFilter(status)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }

    public virtual IQueryable<TEntity> ApplyFilter(
        IQueryable<TEntity> query,
        TFilter filter)
    {
        return query;
    }
}
