using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
using ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Delete;
public class BasketItemDeleteService
    : IBasketItemDeleteService
{
    private readonly IRepository<BasketItem> _itemRepository;
    private readonly BasketItemValidator _itemValidator;

    private readonly BasketItemInvalidationService _itemInvalidation;
    private readonly BasketItemBasketInvalidationService _basketInvalidation;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BasketItemDeleteService> _logger;


    public BasketItemDeleteService(
        IRepository<BasketItem> itemRepository,
        BasketItemValidator itemValidator,
        BasketItemInvalidationService itemInvalidation,
        BasketItemBasketInvalidationService basketValidator,
        IUnitOfWork unitOfWork,
        ILogger<BasketItemDeleteService> logger)
    {
        _itemRepository = itemRepository;
        _itemValidator = itemValidator;
        _itemInvalidation = itemInvalidation;
        _basketInvalidation = basketValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        var item = await _itemValidator
            .GetByIdAsync(id);

        if (!item.IsSuccess)
            return Result<DeleteResponse>.Fail(item.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _itemRepository.Delete(item.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete basket item. Id: {Id}",
                id);

            return Result<DeleteResponse>.Fail(
                EntityErrorResources.BasketItemDeleteFailed);
        }

        _logger.LogInformation(
            "Basket item deleted successfully. BasketItemId: {BasketItemId}",
            item.Value!.Id);

        await ApplyInvalidation(item.Value!);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               item.Value!.Id,
               DateTime.UtcNow
           )
       );
    }

    private async Task ApplyInvalidation(
        BasketItem item)
    {
        var info = BasketItemCacheMapper
            .ToInfo(item);

        await _itemInvalidation
            .InvalidateDeleteAsync(info);

        await _basketInvalidation
            .InvalidateDeleteAsync(info);
    }
}