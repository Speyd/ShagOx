using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
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


    public virtual async Task<long> GetCountAsync(
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Set<TEntity>()
            .WithStatusFilter(status)
            .LongCountAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        IQueryable<TEntity> query = _db.Set<TEntity>();

        query = ApplyIncludes(query);

        return await query
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x => x.Id == id, 
                cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdIncludeAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        IQueryable<TEntity> query = _db.Set<TEntity>();

        query = ApplyIncludes(query);

        return await query
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public virtual async Task<PagedResult<TEntity>> GetPagedAsync(
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        IQueryable<TEntity> query = _db.Set<TEntity>();
           
        query = ApplyIncludes(query);

        return await query
            .WithStatusFilter(status)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }
}
