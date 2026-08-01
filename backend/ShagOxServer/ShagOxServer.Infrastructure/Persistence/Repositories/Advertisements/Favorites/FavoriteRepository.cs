using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites;
public class FavoriteRepository 
    : BaseRepository, IFavoriteRepository
{
    public FavoriteRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<Favorite?> GetByIdAsync(int id)
    {
        return await _db.Favorites
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Add(Favorite favorite)
    {
        _db.Favorites.Add(favorite);
    }

    public void Delete(Favorite favorite)
    {
        _db.Favorites.Remove(favorite);
    }

    public bool Update(Favorite favorite)
    {
        _db.Favorites.Update(favorite);
        return true;
    }
}