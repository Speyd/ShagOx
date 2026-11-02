using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
public interface IBasketItemExistsRepository
    : IExistsRepository<BasketItem>, IExistsOwnerRepository
{
    Task<bool> ExistsAsync(
        long advertisementVariantId,
        long basketId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsByBasketAsync(
        long itemId,
        long basketId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsByAdvertisementVariantAsync(
        long itemId,
        long advertisementVariantId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
