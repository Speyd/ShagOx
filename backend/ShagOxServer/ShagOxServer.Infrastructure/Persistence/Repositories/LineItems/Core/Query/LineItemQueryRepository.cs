using ShagOxServer.Application.Interfaces.Repositories.Base.Items.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.LineItems.Core;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.Core.Query;
public abstract partial class LineItemQueryRepository<TEntity, TFilter>
    : SearchRepository<TEntity, TFilter>,
      ILineItemQueryRepository<TEntity, TFilter>
    where TEntity : LineItem
    where TFilter : BaseFilter
{
    public LineItemQueryRepository(
        ReplicaDbContext db)
        : base(db)
    {
    }

    public async Task<PagedResult<TEntity>> GetByDependencyAsync(
        long advertisementId,
        long? advertisementVariantId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        var query = ApplyIncludes(_db.Set<TEntity>())
            .WithStatusFilter(status);

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

        return await query.ToPagedResultAsync(
            pagination,
            x => x.Id,
            cancellationToken);
    }

    public async Task<PagedResult<TEntity>> GetByParentAsync(
        long parentId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        var query = ApplyIncludes(_db.Set<TEntity>());

        return await query
            .WithStatusFilter(status)
            .Where(x => x.ParentId == parentId)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }

    public abstract Task<PagedResult<TEntity>> GetPagedAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
