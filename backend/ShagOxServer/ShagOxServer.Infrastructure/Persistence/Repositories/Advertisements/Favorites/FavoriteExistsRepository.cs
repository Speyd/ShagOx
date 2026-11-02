using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites;
public class FavoriteExistsRepository
    : ExistsRepository<AdvertisementVariant>,
      IFavoriteExistsRepository
{
    public FavoriteExistsRepository(
        AppDbContext db)
        : base(db)
    { }

    public async Task<bool> IsOwnerAsync(
        long entityId,
        long userId,
        CancellationToken cancellationToken = default)
    {
        return await _db.Favorites
            .AnyAsync(x =>
                x.Id == entityId &&
                x.Advertisement.OwnerId == userId,
                cancellationToken);
    }
}
