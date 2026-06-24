using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities;
using ShagOxServer.Infrastructure.Interfaces;

namespace ShagOxServer.Infrastructure.Persistence.Repositories;

public class AdvertisementRepository : BaseRepository, IAdvertisementRepository
{
    public AdvertisementRepository(AppDbContext db) 
        : base(db)
    {}

    public async Task AddAsync(Advertisement advertisement)
    {
        await _db.Advertisements.AddAsync(advertisement);

        await _db.SaveChangesAsync();
    }

    public async Task<Advertisement?> GetByIdAsync(int id)
    {
        return await _db.Advertisements.FirstOrDefaultAsync(a => a.Id == id);
    }
}
