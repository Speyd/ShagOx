using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base;
public class QueryRepository<TEntity, TFilter>
    : RepositoryContext, 
    IQueryRepository<TEntity, TFilter>
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    public QueryRepository(AppDbContext db)
        : base(db)
    {
    }

    public virtual async Task<TEntity?> GetByIdAsync(
        long id)
    {
        ApplyIncludes(_db.Set<TEntity>());

        return await _db.Set<TEntity>()
            .FindAsync(id);
    }

    public virtual async Task<PagedResult<TEntity>> GetPagedAsync(
        PaginationParams pagination)
    {
        ApplyIncludes(_db.Set<TEntity>());

        return await _db.Set<TEntity>()
            .ToPagedResultAsync(pagination);
    }

    public virtual async Task<PagedResult<TEntity>> SearchAsync(
        TFilter filter,
        PaginationParams pagination)
    {
        ApplyIncludes(_db.Set<TEntity>());
        ApplyFilter(_db.Set<TEntity>(), filter);

        return await _db.Set<TEntity>()
            .ToPagedResultAsync(pagination);
    }

    protected virtual IQueryable<TEntity> ApplyIncludes(
        IQueryable<TEntity> query)
    {
        return query;
    }

    protected virtual IQueryable<TEntity> ApplyFilter(
        IQueryable<TEntity> query,
        TFilter filter)
    {
        return query;
    }
}