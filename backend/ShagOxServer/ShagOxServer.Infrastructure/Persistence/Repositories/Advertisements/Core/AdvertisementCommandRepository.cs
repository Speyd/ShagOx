using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core;
public class AdvertisementCommandRepository
    : CommandRepository<Advertisement>,
    IAdvertisementCommandRepository
{
    public AdvertisementCommandRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
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
