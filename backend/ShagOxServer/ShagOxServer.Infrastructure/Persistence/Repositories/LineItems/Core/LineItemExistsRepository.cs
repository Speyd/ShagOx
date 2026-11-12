using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.LineItems.Core;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.Core;
public abstract class LineItemExistsRepository<TEntity>
    : ExistsRepository<TEntity>,
      ILineItemExistsRepository<TEntity>
    where TEntity : LineItem
{
    public LineItemExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public Task<bool> ExistsAsync(
        long advertisementId,
        long? advertisementVariantId,
        long parentId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        var query = _db.Set<TEntity>()
            .WithStatusFilter(status)
            .Where(x => x.ParentId == parentId);

        if (advertisementVariantId.HasValue)
        {
            query = query.Where(x =>
                x.AdvertisementVariantId == advertisementVariantId);
        }
        else
        {
            query = query.Where(x =>
                x.AdvertisementId == advertisementId);
        }

        return query.AnyAsync(cancellationToken);
    }

    public async Task<bool> ExistsByParentAsync(
        long itemId,
        long parentId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Set<TEntity>()
            .WithStatusFilter(status)
            .AnyAsync(c =>
                c.Id == itemId &&
                c.ParentId == parentId
            );
    }

    public abstract Task<bool> IsOwnerAsync(
        long enityId,
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

}
