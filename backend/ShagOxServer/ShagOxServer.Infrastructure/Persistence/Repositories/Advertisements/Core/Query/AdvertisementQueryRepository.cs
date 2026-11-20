using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query.Catalog;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Enum;
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
        ReplicaDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public async Task<Advertisement?> GetByIdShortIncludeAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .ShortWithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.Status.Code == AdvertisementStatus.Active)
            .FirstOrDefaultAsync(x => x.Id == id);
    }



    public async Task<List<Advertisement>> GetByIdsAsync(
        List<long> ids,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.Status.Code == AdvertisementStatus.Active &&
                ids.Contains(x.Id))
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
            .Where(x => x.OwnerId == userId &&
                x.Status.Code == AdvertisementStatus.Active)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken,
                descending: true);
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

    public async Task<PagedResult<Advertisement>> ShortSearchAsync(
        AdvertisementSearchFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .ShortWithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.OwnerId == _userContext.UserId &&
                x.Status.Code == AdvertisementStatus.Active)
            .Filter(filter)
            .ToPagedResultAsync(pagination,
                (x => x.Popularity),
                cancellationToken);
    }


    public async Task<PagedResult<AdvertisementCatalogQueryDto>> GetCatalogPagedAsync(
        long? userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null,
        CatalogSort sort = CatalogSort.Default)
    {
        var query = _db.Advertisements
            .AsNoTracking()
            .ShortWithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.OwnerId == _userContext.UserId &&
                x.Status.Code == AdvertisementStatus.Active);

        if (sort == CatalogSort.Discount)
        {
            // Discounted = previous price above the current one, on the advertisement or a variant.
            query = query.Where(x =>
                (x.PreviousPrice != null && x.Price != null && x.PreviousPrice > x.Price) ||
                x.Variants.Any(v =>
                    v.EntityStatus == EntityStatus.Active &&
                    v.PreviousPrice > v.Price));
        }

        return sort == CatalogSort.Default
            ? await GetCatalogResultAsync(
                userId, query, pagination, cancellationToken)
            : await GetCatalogResultAsync(
                userId, query, pagination, cancellationToken,
                orderBy: x => x.CreatedAt,
                descending: true);
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
            .Where(x => x.OwnerId == _userContext.UserId &&
                x.Status.Code == AdvertisementStatus.Active)
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
        CancellationToken cancellationToken,
        Expression<Func<Advertisement, object>>? orderBy = null,
        bool descending = false)
    {
        var result = await query.ToPagedResultAsync(
            pagination,
            orderBy ?? (x => x.Popularity),
            cancellationToken,
            descending);

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
