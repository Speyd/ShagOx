using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Delete;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Delete;
public class BasketItemDeleteService
    : IBasketItemDeleteService
{
    private readonly IRepository<BasketItem> _itemRepository;
    private readonly BasketItemValidator _itemValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BasketItemDeleteService> _logger;


    public BasketItemDeleteService(
        IRepository<BasketItem> itemRepository,
        BasketItemValidator itemValidator,
        IUnitOfWork unitOfWork,
        ILogger<BasketItemDeleteService> logger)
    {
        _itemRepository = itemRepository;
        _itemValidator = itemValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
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

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               item.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}