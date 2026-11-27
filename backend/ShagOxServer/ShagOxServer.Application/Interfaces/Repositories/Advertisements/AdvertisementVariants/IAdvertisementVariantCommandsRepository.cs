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

    /// <summary>Чи є варіант у замовленнях (тоді його не можна видаляти фізично).</summary>
    Task<bool> HasOrderItemsAsync(
        long variantId,
        CancellationToken cancellationToken = default);

    /// <summary>Прибирає варіант з усіх кошиків.</summary>
    Task<int> RemoveFromBasketsAsync(
        long variantId,
        CancellationToken cancellationToken = default);
}
