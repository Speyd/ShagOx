using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core;
public class AdvertisementQueryRepository 
    : QueryRepository<Advertisement, AdvertisementSearchFilter>, 
      IAdvertisementQueryRepository
{
    public AdvertisementQueryRepository(AppDbContext db)
        : base(db)
    { }

    protected override IQueryable<Advertisement> ApplyIncludes(
        IQueryable<Advertisement> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<Advertisement> ApplyFilter(
        IQueryable<Advertisement> query,
        AdvertisementSearchFilter filter)
    {
        return query.Filter(filter);
    }

    public async Task<List<Advertisement>> GetByIdsAsync(
        List<long> ids)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<PagedResult<Advertisement>> GetBySellerAsync(
        long userId,
        PaginationParams pagination)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => x.SellerId == userId)
            .OrderByDescending(x => x.Popularity)
            .ToPagedResultAsync(pagination);
    }
    public async Task<PagedResult<Advertisement>> GetPurchasedByUserAsync(
        long userId,
        PaginationParams pagination)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => x.BuyerId == userId)
            .OrderByDescending(x => x.Popularity)
            .ToPagedResultAsync(pagination);
    }

    public override async Task<PagedResult<Advertisement>> SearchAsync(
        AdvertisementSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Filter(filter)
            .OrderByDescending(x => x.Popularity)
            .ToPagedResultAsync(pagination);
    }

    public async Task<List<AdvertisementCacheInfo>> GetCacheInfoByStatusAsync(
        long statusId)
    {
        return await _db.Advertisements
            .Where(x => x.StatusId == statusId)
            .SelectCacheInfo()
            .ToListAsync();
    }

    public async Task<List<AdvertisementCacheInfo>> GetCacheInfoByUserAsync(
        long userId)
    {
        return await _db.Advertisements
            .Where(x => x.SellerId == userId ||
                x.BuyerId == userId)
            .SelectCacheInfo()
            .ToListAsync();
    }
}