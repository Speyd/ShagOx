using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Delete;
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


    public BasketItemDeleteService(
        IRepository<BasketItem> itemRepository,
        BasketItemValidator itemValidator,
        IUnitOfWork unitOfWork)
    {
        _itemRepository = itemRepository;
        _itemValidator = itemValidator;
        _unitOfWork = unitOfWork;
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
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               item.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}