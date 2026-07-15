using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements;

public class AdvertisementQueryRepository : BaseRepository, IAdvertisementQueryRepository
{
    public AdvertisementQueryRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<Advertisement?> GetByIdAsync(int id)
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

    public async Task<List<Advertisement>> GetBySellerAsync(
        int userId,
        PaginationParams pagination)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => x.SellerId == userId)
            .OrderByDescending(x => x.Popularity)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }
    public async Task<List<Advertisement>> GetPurchasedByUserAsync(
        int userId,
        PaginationParams pagination)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => x.BuyerId == userId)
            .OrderByDescending(x => x.Popularity)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }

    public async Task<List<Advertisement>> GetPagedAsync(
        PaginationParams pagination)
    {
        return await _db.Advertisements
            .WithIncludes()
            .OrderByDescending(x => x.Popularity)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }

    public async Task<List<Advertisement>> GetByCategoryAsync(
        int categoryId,
        PaginationParams pagination)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => x.CategoryId == categoryId)
            .OrderByDescending(x => x.Popularity)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }

    public async Task<List<Advertisement>> Search(
        AdvertisementSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Filter(filter)
            .OrderByDescending(x => x.Popularity)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }
}
