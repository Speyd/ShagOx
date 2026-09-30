using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
public interface IBasketItemExistsRepository
    : IExistsRepository<BasketItem>, IExistsOwnerRepository
{
    Task<bool> ExistsAsync(
        long advertisementVariantId,
        long basketId);

    Task<bool> ExistsByBasketAsync(
        long itemId,
        long basketId);

    Task<bool> ExistsByAdvertisementVariantAsync(
        long itemId,
        long advertisementVariantId);
}