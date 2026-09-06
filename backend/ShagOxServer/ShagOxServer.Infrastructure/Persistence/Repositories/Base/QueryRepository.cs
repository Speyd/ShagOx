using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base;
public class QueryRepository<T>
    : RepositoryContext, IQueryRepository<T>
    where T : BaseEntity
{
    public QueryRepository(AppDbContext db)
        : base(db)
    {
    }

    public virtual async Task<T?> GetByIdAsync(
        int id)
    {
        return await _db.Set<T>()
            .FindAsync(id);
    }

    public virtual async Task<PagedResult<T>> GetPagedAsync(
        PaginationParams pagination)
    {
        return await _db.Set<T>()
            .ToPagedResultAsync(pagination);
    }
}