using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Update;
using ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Update;
public class BasketItemUpdateService
    : IBasketItemUpdateService
{
    private readonly IRepository<BasketItem> _itemRepository;
    private readonly BasketItemValidator _itemValidator;

    private readonly IUnitOfWork _unitOfWork;


    public BasketItemUpdateService(
        IRepository<BasketItem> itemRepository,
        BasketItemValidator itemValidator,
        IUnitOfWork unitOfWork)
    {
        _itemRepository = itemRepository;
        _itemValidator = itemValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int itemId,
        BasketItemUpdateRequest request)
    {
        var item = await _itemValidator
            .GetByIdAsync(itemId);
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
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<UpdateResponse>.Success(result);
    }
}