using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base;
public class ExistsRepository<TEntity>
    : RepositoryContext<TEntity>, IExistsRepository<TEntity>
    where TEntity : BaseEntity
{
    public ExistsRepository(ReplicaDbContext db)
        : base(db)
    {
    }


    public virtual async Task<bool> ExistsByIdAsync(
        long id)
    {
        return await _db.Set<TEntity>()
            .AnyAsync(x => EF.Property<int>(x, "Id") == id);
    }
}