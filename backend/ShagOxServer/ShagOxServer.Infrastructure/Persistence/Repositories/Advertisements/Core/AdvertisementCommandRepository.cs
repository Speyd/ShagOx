using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core;
public class AdvertisementCommandRepository
    : IAdvertisementCommandRepository
{
    private readonly AppDbContext _db;

    public AdvertisementCommandRepository(
        AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> DecreaseStockAsync(
        long advertId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        var affected = await _db.Advertisements
            .Where(x =>
                x.Id == advertId &&
                x.Stock >= quantity)
            .ExecuteUpdateAsync(
                setters => setters.SetProperty(
                    x => x.Stock,
                    x => x.Stock - quantity),
                cancellationToken);

        return affected > 0;
    }

    public async Task<bool> IncreaseStockAsync(
        long advertId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));

        var affected = await _db.Advertisements
            .Where(x => x.Id == advertId)
            .ExecuteUpdateAsync(
                setters => setters.SetProperty(
                    x => x.Stock,
                    x => x.Stock + quantity),
                cancellationToken);

        return affected > 0;
    }
}
