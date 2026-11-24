namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
public interface IAdvertisementVariantCommandsRepository
{
    Task<bool> DecreaseStockAsync(
        long variantId,
        int quantity,
        CancellationToken cancellationToken = default);

    Task<bool> IncreaseStockAsync(
        long variantId,
        int quantity,
        CancellationToken cancellationToken = default);
}
