using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base;
public class QueryRepository<TEntity, TFilter>
    : RepositoryContext<TEntity>, 
    IQueryRepository<TEntity, TFilter>
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    public QueryRepository(ReplicaDbContext db)
        : base(db)
    {
    }

    public virtual async Task<TEntity?> GetByIdAsync(
        long id)
    {
        IQueryable<TEntity> query = _db.Set<TEntity>();

        query = ApplyIncludes(query);

        return await query.FirstOrDefaultAsync(
            x => x.Id == id);
    }

    public virtual async Task<PagedResult<TEntity>> GetPagedAsync(
        PaginationParams pagination)
    {
        IQueryable<TEntity> query = _db.Set<TEntity>();
        query = ApplyIncludes(query);

        return await query
            .ToPagedResultAsync(pagination);
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