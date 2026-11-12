using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.LineItems.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Items.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Base.Items;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.Core.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.Core.Query;
public abstract partial class LineItemQueryRepository<TEntity, TFilter>
    : SearchRepository<TEntity, TFilter>,
      ILineItemQueryRepository<TEntity, TFilter>
    where TEntity : LineItem
    where TFilter : BaseFilter
{
    public async Task<List<LineItemCacheInfo>> GetCacheInfosByParentAsync(
        long parentId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        return await _db.Set< TEntity>()
            .WithStatusFilter(status)
            .Where(x => x.ParentId == parentId)
            .SelectCacheInfo(status)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<LineItemCacheInfo>> GetCacheInfosByAdvertisementAsync(
        long advertisementId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        return await _db.Set<TEntity>()
            .WithStatusFilter(status)
           .Where(x =>
                x.AdvertisementId == advertisementId)
           .SelectCacheInfo(status)
           .ToListAsync(cancellationToken);
    }

    public async Task<List<LineItemCacheInfo>> GetCacheInfosByAdvertisementVariantAsync(
        long advertisementVariantId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        return await _db.Set<TEntity>()
            .WithStatusFilter(status)
           .Where(x =>
                x.AdvertisementVariantId == advertisementVariantId)
           .SelectCacheInfo(status)
           .ToListAsync(cancellationToken);
    }
}
