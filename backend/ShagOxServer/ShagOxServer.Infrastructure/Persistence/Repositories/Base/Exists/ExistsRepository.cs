using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

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
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Set<TEntity>()
            .WithStatusFilter(status)
            .AnyAsync(x => EF.Property<int>(x, "Id") == id,
                cancellationToken);
    }
}
