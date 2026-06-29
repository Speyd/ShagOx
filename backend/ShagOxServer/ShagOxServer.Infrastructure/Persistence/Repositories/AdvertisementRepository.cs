using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories;

public class AdvertisementRepository : BaseRepository, IAdvertisementRepository
{
    public AdvertisementRepository(AppDbContext db) 
        : base(db)
    {}

    private IQueryable<Advertisement> Query()
    {
        return _db.Advertisements
            .Include(x => x.Currency)
            .Include(x => x.Category)
            .Include(x => x.Seller)
            .Include(x => x.Images);
    }

    public async Task AddAsync(Advertisement advertisement)
    {
        await _db.Advertisements.AddAsync(advertisement);

        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Advertisement advertisement)
    {
        _db.Advertisements.Remove(advertisement);

        await _db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Advertisement advertisement)
    {
        _db.Advertisements.Update(advertisement);

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<List<Advertisement>> GetPagedAsync(int page, int pageSize)
    {
        return await Query()
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Advertisement>> GetByCategoryAsync(int categoryId)
    {
        return await Query()
            .Where(x => x.CategoryId == categoryId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Advertisement>> SearchAsync(string query)
    {
        return await Query()
            .Where(x =>
                x.Title.Contains(query) ||
                x.Description.Contains(query))
            .OrderByDescending(x => x.Popularity)
            .ToListAsync();
    }

    public async Task<bool> IsOwnerAsync(int adId, int userId)
    {
        var result = await _db.Advertisements.AnyAsync(x =>
           x.Id == adId && x.SellerId == userId);

        return result;
    }

    public async Task<Advertisement?> GetByIdAsync(int id)
    {
        return await Query()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Advertisement?> GetSellerAdvertisementsAsync(int userId)
    {
        return await Query()
            .FirstOrDefaultAsync(x => x.SellerId == userId);
    }
    public async Task<Advertisement?> GetPurchasedAdvertisementsAsync(int userId)
    {
        return await Query()
            .FirstOrDefaultAsync(x => x.BuyerId == userId);
    }
}
