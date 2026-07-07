using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Advertisements.Favorites;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites;
public class FavoriteQueryRepository : BaseRepository, IFavoriteQueryRepository
{
    public FavoriteQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<Favorite?> GetByIdAsync(int id)
    {
        return await _db.Favorites
            .WithIncludes()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> CountByAdvertisementIdAsync(
        int advertisementId)
    {
        return await _db.Favorites
             .WithIncludes()
             .Where(x => x.AdvertisementId == advertisementId)
             .CountAsync();
    }

    public async Task<List<Favorite>> GetByUserIdAsync(int usderId)
    {
        return await _db.Favorites
             .WithIncludes()
             .Where(x => x.UserId == usderId)
             .ToListAsync();
    }
}