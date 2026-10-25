using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
public class ExistsRepository<TEntity>
    : RepositoryContext<TEntity>, IExistsRepository<TEntity>
    where TEntity : BaseEntity
{
    public ExistsRepository(AppDbContext db)
        : base(db)
    {
    }


    public virtual async Task<bool> ExistsByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return await _db.Set<TEntity>()
            .AnyAsync(x => EF.Property<int>(x, "Id") == id,
                cancellationToken);
    }
}
