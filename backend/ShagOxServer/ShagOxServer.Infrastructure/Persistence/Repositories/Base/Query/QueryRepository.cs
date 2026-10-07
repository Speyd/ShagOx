using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
public class QueryRepository<TEntity>
    : RepositoryContext<TEntity>, 
    IQueryRepository<TEntity>
    where TEntity : BaseEntity
{
    public QueryRepository(ReplicaDbContext db)
        : base(db)
    {
    }

    public virtual async Task<TEntity?> GetByIdAsync(
       long id)
    {
        return await _db.Set<TEntity>()
            .FindAsync(id);
    }

    public virtual async Task<TEntity?> GetByIdIncludeAsync(
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
}
