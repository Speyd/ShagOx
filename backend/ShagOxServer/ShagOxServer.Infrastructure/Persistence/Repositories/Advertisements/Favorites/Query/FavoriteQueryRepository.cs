using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Query;
public partial class FavoriteQueryRepository 
    : SearchRepository<Favorite, FavoriteSearchFilter>, 
      IFavoriteQueryRepository
{
    public FavoriteQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<int> CountByAdvertisementAsync(
        long advertisementId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Favorites
             .WithIncludes()
             .WithStatusFilter(status)
             .Where(x => x.AdvertisementId == advertisementId)
             .CountAsync(cancellationToken);
    }

    public async Task<PagedResult<Favorite>> GetByUserAsync(
        long usderId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Favorites
             .WithIncludes()
             .WithStatusFilter(status)
             .Where(x => x.OwnerId == usderId)
             .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }
}
