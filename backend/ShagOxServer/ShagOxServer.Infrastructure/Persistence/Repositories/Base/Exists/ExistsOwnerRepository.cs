using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Base.Owner;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

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
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Set<TEntity>()
           .WithStatusFilter(status)
           .AnyAsync(x =>
               x.Id == entityId &&
               x.OwnerId == userId
           );
    }
}
