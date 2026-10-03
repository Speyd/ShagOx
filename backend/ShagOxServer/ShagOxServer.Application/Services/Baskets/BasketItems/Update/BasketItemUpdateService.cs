using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
using ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Update;
public class BasketItemUpdateService
    : IBasketItemUpdateService
{
    private readonly IRepository<BasketItem> _itemRepository;
    private readonly BasketItemValidator _itemValidator;

    private readonly BasketItemInvalidationService _itemInvalidation;
    private readonly BasketItemBasketInvalidationService _basketInvalidation;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BasketItemUpdateService> _logger;


    public BasketItemUpdateService(
        IRepository<BasketItem> itemRepository,
        BasketItemValidator itemValidator,
        BasketItemInvalidationService itemInvalidation,
        BasketItemBasketInvalidationService basketInvalidation,
        IUnitOfWork unitOfWork,
        ILogger<BasketItemUpdateService> logger)
    {
        _itemRepository = itemRepository;
        _itemValidator = itemValidator;
        _itemInvalidation = itemInvalidation;
        _basketInvalidation = basketInvalidation;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long itemId,
        BasketItemUpdateRequest request)
    {
        var item = await _itemValidator
            .GetByIdWithIncludesAsync(itemId);

        if (!item.IsSuccess)
            return Result<UpdateResponse>.Fail(item.Error);

        var updatedCount = BasketItemUpdater
            .ApplyUpdates(item.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _itemRepository.Update(item.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update basket item. Id: {Id}",
                itemId);

            return Result<UpdateResponse>
                .Fail(EntityErrorResources.BasketItemUpdateFailed);
        }

        _logger.LogInformation(
           "Basket item update successfully. BasketItemId: {BasketItemId}",
           item.Value!.Id);

        await ApplyInvalidation(item.Value!);

        return Result<UpdateResponse>.Success(result);
    }

    private async Task ApplyInvalidation(
        BasketItem item)
    {
        var info = BasketItemCacheMapper
            .ToInfo(item);

        await _itemInvalidation
            .InvalidateUpdateAsync(info);

        await _basketInvalidation
            .InvalidateUpdateAsync(info);
    }
}