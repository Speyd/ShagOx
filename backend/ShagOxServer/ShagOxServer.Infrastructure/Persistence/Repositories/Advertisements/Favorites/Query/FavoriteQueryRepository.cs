using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Query;
public partial class FavoriteQueryRepository 
    : QueryRepository<Favorite, FavoriteSearchFilter>, 
      IFavoriteQueryRepository
{
    public FavoriteQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<int> CountByAdvertisementAsync(
        long advertisementId)
    {
        return await _db.Favorites
             .WithIncludes()
             .Where(x => x.AdvertisementId == advertisementId)
             .CountAsync();
    }

    public async Task<PagedResult<Favorite>> GetByUserAsync(
        long usderId,
        PaginationParams pagination)
    {
        return await _db.Favorites
             .WithIncludes()
             .Where(x => x.UserId == usderId)
             .ToPagedResultAsync(pagination);
    }
}