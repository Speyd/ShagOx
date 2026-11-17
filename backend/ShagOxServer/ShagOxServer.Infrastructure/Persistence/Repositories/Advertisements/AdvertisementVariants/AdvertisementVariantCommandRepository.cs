using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants;
public class AdvertisementVariantCommandRepository
    : IAdvertisementVariantCommandRepository
{
    private readonly AppDbContext _db;

    public AdvertisementVariantCommandRepository(
        AppDbContext db)
    {
        _db = db;
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
}
