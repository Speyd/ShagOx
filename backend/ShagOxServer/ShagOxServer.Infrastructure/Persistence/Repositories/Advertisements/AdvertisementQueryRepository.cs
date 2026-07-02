using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Extensions;

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

    public async Task<List<Advertisement>> GetSellerAdvertisementsAsync(
        int userId,
        int page,
        int pageSize)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => x.SellerId == userId)
            .OrderByDescending(x => x.Popularity)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task<List<Advertisement>> GetPurchasedAdvertisementsAsync(
        int userId,
        int page,
        int pageSize)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => x.BuyerId == userId)
            .OrderByDescending(x => x.Popularity)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Advertisement>> GetPagedAsync(int page, int pageSize)
    {
        return await _db.Advertisements
            .WithIncludes()
            .OrderByDescending(x => x.Popularity)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Advertisement>> GetByCategoryAsync(
        int categoryId,
        int page,
        int pageSize)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => x.CategoryId == categoryId)
            .OrderByDescending(x => x.Popularity)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Advertisement>> SearchByTitle(
        string title,
        int page,
        int pageSize)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => x.Title.Contains(title))
            .OrderByDescending(x => x.Popularity)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Advertisement>> SearchByDescription(
        string query,
        int page,
        int pageSize)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => x.Description.Contains(query))
            .OrderByDescending(x => x.Popularity)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
