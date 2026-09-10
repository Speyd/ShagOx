using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core;
public class AdvertisementQueryRepository 
    : QueryRepository<Advertisement>, 
      IAdvertisementQueryRepository
{
    public AdvertisementQueryRepository(AppDbContext db)
        : base(db)
    { }


    public override async Task<Advertisement?> GetByIdAsync(
        int id)
    {
        return await _db.Advertisements
            .WithIncludes()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Advertisement>> GetByIdsAsync(
        List<int> ids)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<PagedResult<Advertisement>> GetBySellerAsync(
        int userId,
        PaginationParams pagination)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => x.SellerId == userId)
            .OrderByDescending(x => x.Popularity)
            .ToPagedResultAsync(pagination);
    }
    public async Task<PagedResult<Advertisement>> GetPurchasedByUserAsync(
        int userId,
        PaginationParams pagination)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => x.BuyerId == userId)
            .OrderByDescending(x => x.Popularity)
            .ToPagedResultAsync(pagination);
    }

    public override async Task<PagedResult<Advertisement>> GetPagedAsync(
        PaginationParams pagination)
    {
        return await _db.Advertisements
            .WithIncludes()
            .OrderByDescending(x => x.Popularity)
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<Advertisement>> Search(
        AdvertisementSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Filter(filter)
            .OrderByDescending(x => x.Popularity)
            .ToPagedResultAsync(pagination);
    }
}