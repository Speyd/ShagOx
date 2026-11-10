using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query.Catalog;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Query;
public partial class AdvertisementQueryRepository 
: SearchRepository<Advertisement, AdvertisementSearchFilter>, 
      IAdvertisementQueryRepository
{
    public AdvertisementQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<List<Advertisement>> GetByIdsAsync(
        List<long> ids,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<PagedResult<Advertisement>> GetBySellerAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.OwnerId == userId)
            .OrderByDescending(x => x.Popularity)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }
    public async Task<PagedResult<Advertisement>> GetPurchasedByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.BuyerId == userId)
            .OrderByDescending(x => x.Popularity)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }

    public override async Task<PagedResult<Advertisement>> SearchAsync(
        AdvertisementSearchFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .WithIncludes()
            .WithStatusFilter(status)
            .Filter(filter)
            .OrderByDescending(x => x.Popularity)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }

    public async Task<PagedResult<Advertisement>> ShortSearchAsync(
        AdvertisementSearchFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .ShortWithIncludes()
            .WithStatusFilter(status)
            .Filter(filter)
            .OrderByDescending(x => x.Popularity)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }


    public async Task<PagedResult<AdvertisementCatalogQueryDto>> GetCatalogPagedAsync(
     PaginationParams pagination,
     CancellationToken cancellationToken = default,
     EntityStatus? status = null)
    {
        var query = _db.Advertisements
            .AsNoTracking()
            .ShortWithIncludes()
            .WithStatusFilter(status)
            .OrderByDescending(x => x.Popularity);

        return await GetCatalogResultAsync(
            query, pagination, cancellationToken);
    }

    public async Task<PagedResult<AdvertisementCatalogQueryDto>> CatalogSearchAsync(
        AdvertisementSearchFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        var query = _db.Advertisements
            .AsNoTracking()
            .ShortWithIncludes()
            .WithStatusFilter(status)
            .Filter(filter)
            .OrderByDescending(x => x.Popularity);

        return await GetCatalogResultAsync(
            query, pagination, cancellationToken);
    }

    private async Task<PagedResult<AdvertisementCatalogQueryDto>> GetCatalogResultAsync(
        IQueryable<Advertisement> query,
        PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await query.ToPagedResultAsync(
            pagination,
            x => x.Id,
            cancellationToken);

        var ids = result.Items.Select(x => x.Id).ToList();

        var reviews = await _db.ReviewComments
            .AsNoTracking()
            .Where(r => ids.Contains(r.CommentableEntityId))
            .GroupBy(r => r.CommentableEntityId)
            .Select(g => new
            {
                AdvertisementId = g.Key,
                Count = g.Count(),
                Average = g.Average(r => (double?)r.Grade) ?? 0
            })
            .ToDictionaryAsync(
                x => x.AdvertisementId,
                cancellationToken);

        var items = result.Items.Select(x =>
        {
            reviews.TryGetValue(x.Id, out var review);

            return new AdvertisementCatalogQueryDto(
                x,
                review?.Count ?? 0,
                (float)(review?.Average ?? 0));
        }).ToList();

        return new PagedResult<AdvertisementCatalogQueryDto>(
            items,
            result.TotalCount,
            new PaginationParams(result.Page, result.PageSize));
    }
}
