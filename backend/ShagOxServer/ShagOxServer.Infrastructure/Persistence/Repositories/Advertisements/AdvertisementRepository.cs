using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Advertisements.Advertisement;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements;

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


    public async Task<Advertisement?> GetByIdAsync(int id)
    {
        return await _db.Advertisements
            .FirstOrDefaultAsync(x => x.Id == id);
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
}
