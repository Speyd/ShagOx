using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Delete;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Update;
using ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Update;
public class BasketItemUpdateService
    : BaseUpdateService<BasketItem>,
    IBasketItemUpdateService
{
    private readonly IRepository<BasketItem> _itemRepository;
    private readonly BasketItemValidator _itemValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BasketItemUpdateService> _logger;


    public BasketItemUpdateService(
        IRepository<BasketItem> itemRepository,
        BasketItemValidator itemValidator,
        IBasketItemDeleteService itemDeleteService,
        IUnitOfWork unitOfWork,
        ILogger<BasketItemUpdateService> logger
    )
        : base(itemDeleteService, itemValidator, logger, unitOfWork)
    {
        _itemRepository = itemRepository;
        _itemValidator = itemValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long itemId,
        BasketItemUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        var item = await _itemValidator
            .GetByIdIncludeAsync(itemId, cancellationToken);

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
        catch (DbUpdateConcurrencyException ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogWarning(
                ex,
                "Concurrency conflict while updating basket item." +
                " Id: {Id}",
                itemId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.ConcurrencyConflictMessage);
        }
        catch (Exception ex)
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

        return Result<UpdateResponse>.Success(result);
    }
}
