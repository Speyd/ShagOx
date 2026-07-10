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

    public void Add(Advertisement advertisement)
    {
        _db.Advertisements.Add(advertisement);
    }

    public void Delete(Advertisement advertisement)
    {
        _db.Advertisements.Remove(advertisement);
    }

    public bool Update(Advertisement advertisement)
    {
        _db.Advertisements.Update(advertisement);
        return true;
    }
}