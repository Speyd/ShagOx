namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
public interface IAdvertisementCommandRepository
{
    Task<bool> DecreaseStockAsync(
        long advertId,
        int quantity,
        CancellationToken cancellationToken = default);

    Task<bool> IncreaseStockAsync(
        long advertId,
        int quantity,
        CancellationToken cancellationToken = default);
}
