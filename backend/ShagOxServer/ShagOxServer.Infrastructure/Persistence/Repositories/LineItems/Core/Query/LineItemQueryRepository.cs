using LinqKit;
using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base.Items.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
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
        ReplicaDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
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

    public async Task<List<TEntity>> GetByParentAndDependenciesAsync(
        long parentId,
        List<(long AdvertId, long? VariantId)> dependencies,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        if (dependencies.Count == 0)
            return [];

        var variantIds = dependencies
            .Where(x => x.VariantId.HasValue)
            .Select(x => x.VariantId!.Value)
            .Distinct()
            .ToArray();

        var advertIds = dependencies
            .Where(x => !x.VariantId.HasValue)
            .Select(x => x.AdvertId)
            .Distinct()
            .ToArray();

        return await ApplyIncludes(_db.Set<TEntity>())
            .WithStatusFilter(status)
            .Where(x => x.ParentId == parentId &&
                (x.AdvertisementVariantId.HasValue
                    ? variantIds.Contains(x.AdvertisementVariantId.Value)
                    : advertIds.Contains(x.AdvertisementId)))
            .ToListAsync(cancellationToken);
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
