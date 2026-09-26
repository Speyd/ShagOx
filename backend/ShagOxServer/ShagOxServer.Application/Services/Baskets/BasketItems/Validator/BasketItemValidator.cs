using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
public class BasketItemValidator
    : BaseValidator<BasketItem>
{
    private readonly IBasketItemExistsRepository _itemExistsRepository;
    private readonly IBasketItemQueryRepository _itemQeuryRepository;


    public BasketItemValidator(
        IRepository<BasketItem> itemRepository,
        IBasketItemExistsRepository itemExistsRepository,
        IBasketItemQueryRepository itemQeuryRepository
    ) : base(itemRepository, itemExistsRepository)
    {
        _itemExistsRepository = itemExistsRepository;
        _itemQeuryRepository = itemQeuryRepository;
    }

    public async Task<Result<BasketItem>> GetByIdWithIncludesAsync(
        int id)
    {
        var item = await _itemQeuryRepository
            .GetByIdAsync(id);

        if (item is null)
        {
            return Result<BasketItem>.NotFound(
                EntityNamesResources.BasketItem);
        }

        return Result<BasketItem>.Success(item);
    }

    public async Task<Result<bool>> ExistsAsync(
        int advertisementId,
        int basketId)
    {
        if (!await _itemExistsRepository
                .ExistsAsync(advertisementId, basketId))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.BasketItem);
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
            return Result<bool>.AlreadyExists(
                EntityNamesResources.BasketItem);
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
            return Result<bool>.NotFound(
                EntityNamesResources.BasketItem);
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
            return Result<bool>.AlreadyExists(
                EntityNamesResources.BasketItem);
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
            return Result<bool>.NotFound(
                EntityNamesResources.BasketItem);
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
            return Result<bool>.AlreadyExists(
                EntityNamesResources.BasketItem);
        }

        return Result<bool>.Success(true);
    }
}