using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base;
public class CommandRepository<TEntity>
    : RepositoryContext<TEntity>,
    ICommandRepository<TEntity>
    where TEntity : BaseEntity
{
    public CommandRepository(AppDbContext db)
        : base(db)
    {
    }


    public async Task<bool> SoftDeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var affectedRows = await _db.Set<TEntity>()
            .Where(x => x.Id == id &&
                        x.EntityStatus != EntityStatus.Deleted)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(
                        x => x.EntityStatus,
                        EntityStatus.Deleted)
                    .SetProperty(
                        x => x.DeletedAt,
                        DateTime.UtcNow),
                cancellationToken);

        return affectedRows > 0;
    }
}
