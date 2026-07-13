using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements;
public class AdvertisementExistsRepository : BaseRepository, IAdvertisementExistsRepository
{
    public AdvertisementExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsById(int Id)
    {
        var result = await _db.Advertisements
            .AnyAsync(x => x.Id == Id);

        return result;
    }

    public async Task<bool> IsOwnerAsync(int adId, int userId)
    {
        var result = await _db.Advertisements.AnyAsync(x =>
           (x.Id == adId && x.SellerId == userId));

        return result;
    }
}
