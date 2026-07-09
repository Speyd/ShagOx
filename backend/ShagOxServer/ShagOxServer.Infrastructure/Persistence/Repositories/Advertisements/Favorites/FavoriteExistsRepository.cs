using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites;
public class FavoriteExistsRepository : BaseRepository, IFavoriteExistsRepository
{
    public FavoriteExistsRepository(AppDbContext db)
        : base(db)
    { }
}