using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
public class BasketItemValidator
    : BaseValidator<BasketItem>
{
    private readonly IBasketItemExistsRepository _itemExistsRepository;


    public BasketItemValidator(
        IRepository<BasketItem> repository,
        IBasketItemExistsRepository itemExistsRepository,
        IBasketItemQueryRepository itemQeuryRepository,
        IDataCacheService cache
    ) 
        : base(repository, itemQeuryRepository, itemExistsRepository, cache)
    {
        _itemExistsRepository = itemExistsRepository;
    }

    public async Task<Result<bool>> ExistsAsync(
        long advertisementid,
        long? advertisementVariantId,
        long basketId,
        CancellationToken cancellationToken = default)
    {
        if (!await _itemExistsRepository
                .ExistsAsync(
                    advertisementid,
                    advertisementVariantId, 
                    basketId, 
                    cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.BasketItem);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        long advertisementid,
        long? advertisementVariantId,
        long basketId,
        CancellationToken cancellationToken = default)
    {
        if (await _itemExistsRepository
                .ExistsAsync(
                    advertisementid, advertisementVariantId,
                    basketId, 
                    cancellationToken))
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
                .ExistsByParentAsync(itemId, 
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
                .ExistsByParentAsync(itemId, 
                    basketId, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.BasketItem);
        }

        return Result<bool>.Success(true);
    }
}
