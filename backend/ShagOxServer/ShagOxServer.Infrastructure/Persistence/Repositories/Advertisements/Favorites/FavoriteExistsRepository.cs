using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites;
public class FavoriteExistsRepository 
    : RepositoryContext, IFavoriteExistsRepository
{
    public FavoriteExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        var result = await _db.Favorites
            .AnyAsync(x => x.Id == id);

        return result;
    }
}