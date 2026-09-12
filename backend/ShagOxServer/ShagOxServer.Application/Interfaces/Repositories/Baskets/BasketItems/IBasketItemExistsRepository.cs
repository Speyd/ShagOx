using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
public interface IBasketItemExistsRepository
    : IExistsRepository<BasketItem>, IExistsOwnerRepository
{
    Task<bool> ExistsAsync(
        int advertisementId,
        int basketId);

    Task<bool> ExistsByBasketAsync(
        int itemId,
        int basketId);

    Task<bool> ExistsByAdvertisementAsync(
        int itemId,
        int advertisementId);
}