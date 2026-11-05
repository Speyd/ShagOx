using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base.Validator;
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
        IRepository<BasketItem> repository,
        IBasketItemExistsRepository itemExistsRepository,
        IBasketItemQueryRepository itemQeuryRepository,
        IDataCacheService cache
    ) 
        : base(repository, itemQeuryRepository, itemExistsRepository, cache)
    {
        _itemExistsRepository = itemExistsRepository;
        _itemQeuryRepository = itemQeuryRepository;
    }

    public async Task<Result<bool>> ExistsAsync(
        long advertisementVariantId,
        long basketId,
        CancellationToken cancellationToken = default)
    {
        if (!await _itemExistsRepository
                .ExistsAsync(advertisementVariantId, 
                    basketId, cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.BasketItem);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        long advertisementVariantId,
        long basketId,
        CancellationToken cancellationToken = default)
    {
        if (await _itemExistsRepository
                .ExistsAsync(advertisementVariantId,
                    basketId, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.BasketItem);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByBasketAsync(
        long itemId,
        long basketId,
        CancellationToken cancellationToken = default)
    {
        if (!await _itemExistsRepository
                .ExistsByBasketAsync(itemId, 
                    basketId, cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.BasketItem);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByBasketAsync(
        long itemId,
        long basketId,
        CancellationToken cancellationToken = default)
    {
        if (await _itemExistsRepository
                .ExistsByBasketAsync(itemId, 
                    basketId, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.BasketItem);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByAdvertisementAsync(
        long itemId,
        long advertisementVariantId,
        CancellationToken cancellationToken = default)
    {
        if (!await _itemExistsRepository
                .ExistsByAdvertisementVariantAsync(itemId,
                    advertisementVariantId, cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.BasketItem);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByAdvertisementAsync(
        long itemId,
        long advertisementVariantId,
        CancellationToken cancellationToken = default)
    {
        if (await _itemExistsRepository
                .ExistsByAdvertisementVariantAsync(itemId,
                    advertisementVariantId, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.BasketItem);
        }

        return Result<bool>.Success(true);
    }
}
