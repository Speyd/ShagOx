using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Domain.Base.Owner;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
public class ExistsOwnerRepository<TEntity>
    : ExistsRepository<TEntity>, 
        IExistsOwnerRepository
    where TEntity : OwnedEntity
{
    public ExistsOwnerRepository(AppDbContext db)
        : base(db)
    {
    }


    public virtual async Task<bool> IsOwnerAsync(
        long entityId,
        long userId,
        CancellationToken cancellationToken = default)
    {
        return await _db.Set<TEntity>()
           .AnyAsync(c =>
               c.Id == entityId &&
               c.OwnerId == userId
           );
    }
}
