namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
public interface IAdvertisementVariantCommandRepository
{
    Task<bool> DecreaseStockAsync(
        long variantId,
        int quantity,
        CancellationToken cancellationToken = default);

}
