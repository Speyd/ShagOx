using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites;
public class FavoriteQueryRepository 
    : QueryRepository<Favorite>, 
      IFavoriteQueryRepository
{
    public FavoriteQueryRepository(AppDbContext db)
        : base(db)
    { }


    public override async Task<Favorite?> GetByIdAsync(
        int id)
    {
        return await _db.Favorites
            .WithIncludes()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public override async Task<PagedResult<Favorite>> GetPagedAsync(
        PaginationParams pagination)
    {
        return await _db.Favorites
            .WithIncludes()
            .ToPagedResultAsync(pagination);
    }

    public async Task<int> CountByAdvertisementAsync(
        int advertisementId)
    {
        return await _db.Favorites
             .WithIncludes()
             .Where(x => x.AdvertisementId == advertisementId)
             .CountAsync();
    }

    public async Task<PagedResult<Favorite>> GetByUserAsync(
        int usderId,
        PaginationParams pagination)
    {
        return await _db.Favorites
             .WithIncludes()
             .Where(x => x.UserId == usderId)
             .ToPagedResultAsync(pagination);
    }
}