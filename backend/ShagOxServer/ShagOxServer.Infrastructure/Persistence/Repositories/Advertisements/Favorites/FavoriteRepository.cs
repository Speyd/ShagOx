using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Advertisements.Favorites;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites;
public class FavoriteRepository : BaseRepository, IFavoriteRepository
{
    public FavoriteRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<Favorite?> GetByIdAsync(int id)
    {
        return await _db.Favorites
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Favorite favorite)
    {
        await _db.Favorites.AddAsync(favorite);

        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Favorite favorite)
    {
        _db.Favorites.Remove(favorite);

        await _db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Favorite favorite)
    {
        _db.Favorites.Update(favorite);

        await _db.SaveChangesAsync();
        return true;
    }
}