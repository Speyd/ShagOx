using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites;
public class FavoriteExistsRepository
    : ExistsOwnerRepository<Favorite>,
      IFavoriteExistsRepository
{
    public FavoriteExistsRepository(
        AppDbContext db)
        : base(db)
    { }
}
