using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
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
        PaginationParams pagination)
    {
        IQueryable<TEntity> query = _db.Set<TEntity>();

        query = ApplyIncludes(query);
        query = ApplyFilter(query, filter);

        return await query
            .ToPagedResultAsync(pagination);
    }

    protected virtual IQueryable<TEntity> ApplyFilter(
        IQueryable<TEntity> query,
        TFilter filter)
    {
        return query;
    }
}
