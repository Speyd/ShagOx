using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
public class BasketItemValidator
    : BaseValidator<BasketItem>
{
    private readonly IBasketItemExistsRepository _itemExistsRepository;

    public BasketItemValidator(
        IRepository<BasketItem> itemRepository,
        IBasketItemExistsRepository itemExistsRepository
    ) : base(itemRepository, itemExistsRepository)
    {
        _itemExistsRepository = itemExistsRepository;
    }


    public async Task<Result<bool>> ExistsAsync(
        int advertisementId,
        int basketId)
    {
        if (!await _itemExistsRepository
                .ExistsAsync(advertisementId, basketId))
        {
            return Result<bool>
                .NotFound(typeof(BasketItem));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        int advertisementId,
        int basketId)
    {
        if (await _itemExistsRepository
                .ExistsAsync(advertisementId, basketId))
        {
            return Result<bool>
                .AlreadyExists(typeof(BasketItem));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByBasketAsync(
        int itemId,
        int basketId)
    {
        if (!await _itemExistsRepository
                .ExistsByBasketAsync(itemId, basketId))
        {
            return Result<bool>
                .NotFound(typeof(BasketItem));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByBasketAsync(
        int itemId,
        int basketId)
    {
        if (await _itemExistsRepository
                .ExistsByBasketAsync(itemId, basketId))
        {
            return Result<bool>
                .AlreadyExists(typeof(BasketItem));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByAdvertisementAsync(
        int itemId,
        int advertisementId)
    {
        if (!await _itemExistsRepository
                .ExistsByAdvertisementAsync(itemId, advertisementId))
        {
            return Result<bool>
                .NotFound(typeof(BasketItem));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByAdvertisementAsync(
        int itemId,
        int advertisementId)
    {
        if (await _itemExistsRepository
                .ExistsByAdvertisementAsync(itemId, advertisementId))
        {
            return Result<bool>
                .AlreadyExists(typeof(BasketItem));
        }

        return Result<bool>.Success(true);
    }
}