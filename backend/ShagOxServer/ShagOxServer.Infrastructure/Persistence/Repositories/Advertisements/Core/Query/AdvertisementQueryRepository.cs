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
            .ToPagedResultAsync(pagination,
                (x => x.Popularity),
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
            .ToPagedResultAsync(pagination,
                (x => x.Popularity),
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
            .ToPagedResultAsync(pagination,
                (x => x.Popularity),
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
            .ToPagedResultAsync(pagination,
                (x => x.Popularity),
                cancellationToken);
    }


    public async Task<PagedResult<AdvertisementCatalogQueryDto>> GetCatalogPagedAsync(
        long? userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        var query = _db.Advertisements
            .AsNoTracking()
            .ShortWithIncludes()
            .WithStatusFilter(status);

        return await GetCatalogResultAsync(
            userId,
            query, 
            pagination, 
            cancellationToken
        );
    }

    public async Task<PagedResult<AdvertisementCatalogQueryDto>> CatalogSearchAsync(
        long? userId,
        AdvertisementSearchFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        var query = _db.Advertisements
            .AsNoTracking()
            .ShortWithIncludes()
            .WithStatusFilter(status)
            .Filter(filter);

        return await GetCatalogResultAsync(
            userId,
            query, 
            pagination, 
            cancellationToken
        );
    }

    private async Task<PagedResult<AdvertisementCatalogQueryDto>> GetCatalogResultAsync(
        long? userId,
        IQueryable<Advertisement> query,
        PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await query.ToPagedResultAsync(
            pagination,
            x => x.Popularity,
            cancellationToken);

        var ids = result.Items.Select(x => x.Id).ToList();

        if (ids.Count == 0)
        {
            return new PagedResult<AdvertisementCatalogQueryDto>(
                [],
                result.TotalCount,
                new PaginationParams(result.Page, result.PageSize));
        }

        var reviews = await GetReviewsByAdvertisementIdsAsync(
            ids,
            cancellationToken);

        var favorites = await GetFavoritesByAdvertisementIdsAsync(
            userId,
            ids,
            cancellationToken);

        var baskets = await GetBasketsByAdvertisementIdsAsync(
            userId,
            ids,
            cancellationToken);

        var items = result.Items.Select(x =>
        {
            reviews.TryGetValue(x.Id, out var review);

            return new AdvertisementCatalogQueryDto(
                x,
                review.Count,
                (float)(review.Average),
                favorites.Contains(x.Id),
                baskets.Contains(x.Id));             
        }).ToList();

        return new PagedResult<AdvertisementCatalogQueryDto>(
            items,
            result.TotalCount,
            new PaginationParams(result.Page, result.PageSize));
    }
}
