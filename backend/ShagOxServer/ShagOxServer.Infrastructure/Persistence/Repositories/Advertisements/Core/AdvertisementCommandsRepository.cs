using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core;
public class AdvertisementCommandsRepository
    : CommandRepository<Advertisement>,
    IAdvertisementCommandRepository
{
    private readonly ICacheInvalidationService<
        Advertisement, AdvertisementCacheContext> _entityInvalid;


    public AdvertisementCommandsRepository(
        AppDbContext db,
        IUserContext userContext,
        ICacheInvalidationService<
        Advertisement, AdvertisementCacheContext> entityInvalid
    )
        : base(db, userContext)
    {
        _entityInvalid = entityInvalid;
    }


    public async Task<bool> DecreaseStockAsync(
        long advertId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        var query = _db.Advertisements
            .AsNoTracking()
            .Where(x =>
                x.Id == advertId &&
                x.Stock >= quantity);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters.SetProperty(
                    x => x.Stock,
                    x => x.Stock - quantity),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }

    public async Task<bool> IncreaseStockAsync(
        long advertId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));

        var query = _db.Advertisements
            .AsNoTracking()
            .Where(x => x.Id == advertId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters.SetProperty(
                    x => x.Stock,
                    x => x.Stock + quantity),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }
}
