using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements;

public class AdvertisementRepository : BaseRepository, IAdvertisementRepository
{
    public AdvertisementRepository(AppDbContext db) 
        : base(db)
    {}

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
