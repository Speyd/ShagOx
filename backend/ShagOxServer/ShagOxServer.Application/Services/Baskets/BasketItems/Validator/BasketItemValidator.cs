using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
public class BasketItemValidator
    : BaseValidator<BasketItem>
{
    private readonly IBasketItemExistsRepository _itemExistsRepository;
    private readonly IBasketItemQueryRepository _itemQeuryRepository;


    public BasketItemValidator(
        IBasketItemExistsRepository itemExistsRepository,
        IBasketItemQueryRepository itemQeuryRepository
    ) : base(itemQeuryRepository, itemExistsRepository)
    {
        _itemExistsRepository = itemExistsRepository;
        _itemQeuryRepository = itemQeuryRepository;
    }

    public async Task<Result<BasketItem>> GetByIdWithIncludesAsync(
        long id)
    {
        var item = await _itemQeuryRepository
            .GetByIdIncludeAsync(id);

        if (item is null)
        {
            return Result<BasketItem>.NotFound(
                EntityNamesResources.BasketItem);
        }

        return Result<BasketItem>.Success(item);
    }

    public async Task<Result<bool>> ExistsAsync(
        long advertisementVariantId,
        long basketId)
    {
        if (!await _itemExistsRepository
                .ExistsAsync(advertisementVariantId, basketId))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.BasketItem);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        long advertisementVariantId,
        long basketId)
    {
        if (await _itemExistsRepository
                .ExistsAsync(advertisementVariantId, basketId))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.BasketItem);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByBasketAsync(
        long itemId,
        long basketId)
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
        long itemId,
        long basketId)
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
        long itemId,
        long advertisementVariantId)
    {
        if (!await _itemExistsRepository
                .ExistsByAdvertisementVariantAsync(itemId,
                    advertisementVariantId))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.BasketItem);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByAdvertisementAsync(
        long itemId,
        long advertisementVariantId)
    {
        if (await _itemExistsRepository
                .ExistsByAdvertisementVariantAsync(itemId,
                    advertisementVariantId))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.BasketItem);
        }

        return Result<bool>.Success(true);
    }
}
