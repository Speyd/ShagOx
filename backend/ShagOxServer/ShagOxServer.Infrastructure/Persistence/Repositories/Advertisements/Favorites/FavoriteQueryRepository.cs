using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Advertisements.Core;
using ShagOxServer.Application.DTOs.Advertisements.Favorites.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using Twilio.TwiML.Voice;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites;
public class FavoriteQueryRepository 
    : QueryRepository<Favorite, FavoriteSearchFilter>, 
      IFavoriteQueryRepository
{
    public FavoriteQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<Favorite> ApplyIncludes(
         IQueryable<Favorite> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<Favorite> ApplyFilter(
       IQueryable<Favorite> query,
       FavoriteSearchFilter filter)
    {
        return query.Filter(filter);
    }

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

    public async Task<List<FavoriteCacheInfo>> GetCacheInfoByAdvertisementAsync(
        long advertId)
    {
        return await _db.Favorites
            .Where(x => x.AdvertisementId == advertId)
            .SelectCacheInfo()
            .ToListAsync();
    }
}