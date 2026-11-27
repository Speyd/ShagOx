using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants;
public class AdvertisementVariantCommandsRepository
    : CommandRepository<AdvertisementVariant>,
    IAdvertisementVariantCommandsRepository
{
    private readonly ICacheInvalidationService<
        AdvertisementVariant, AdvertisementVariantCacheContext> _entityInvalid;


    public AdvertisementVariantCommandsRepository(
        AppDbContext db,
        IUserContext userContext,
        ICacheInvalidationService<
            AdvertisementVariant, AdvertisementVariantCacheContext> entityInvalid
    )
        : base(db, userContext)
    {
        _entityInvalid = entityInvalid;
    }


    public async Task<bool> DecreaseStockAsync(
        long variantId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        var query = _db.AdvertisementVariants
            .Where(x =>
                x.Id == variantId &&
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
        long variantId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));

        var query = _db.AdvertisementVariants
            .Where(x => x.Id == variantId);

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

    public async Task<bool> HasOrderItemsAsync(
        long variantId,
        CancellationToken cancellationToken = default)
    {
        return await _db.OrderItems
            .AnyAsync(x => x.AdvertisementVariantId == variantId,
                cancellationToken);
    }

    public async Task<int> RemoveFromBasketsAsync(
        long variantId,
        CancellationToken cancellationToken = default)
    {
        return await _db.BasketItems
            .Where(x => x.AdvertisementVariantId == variantId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
