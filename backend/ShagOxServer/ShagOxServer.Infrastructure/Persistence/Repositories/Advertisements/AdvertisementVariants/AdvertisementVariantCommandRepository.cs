using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants;
public class AdvertisementVariantCommandRepository
    : CommandRepository<AdvertisementVariant>,
    IAdvertisementVariantCommandRepository
{
    public AdvertisementVariantCommandRepository(
        AppDbContext db
    )
        : base(db)
    { 
    }

    public async Task<bool> DecreaseStockAsync(
        long variantId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        var affected = await _db.AdvertisementVariants
            .Where(x =>
                x.Id == variantId &&
                x.Stock >= quantity)
            .ExecuteUpdateAsync(
                setters => setters.SetProperty(
                    x => x.Stock,
                    x => x.Stock - quantity),
                cancellationToken);

        return affected > 0;
    }

    public async Task<bool> IncreaseStockAsync(
        long variantId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));

        var affected = await _db.AdvertisementVariants
            .Where(x => x.Id == variantId)
            .ExecuteUpdateAsync(
                setters => setters.SetProperty(
                    x => x.Stock,
                    x => x.Stock + quantity),
                cancellationToken);

        return affected > 0;
    }
}
