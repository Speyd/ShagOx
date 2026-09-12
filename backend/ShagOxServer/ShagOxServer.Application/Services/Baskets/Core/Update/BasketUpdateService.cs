using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.Core.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Update;
using ShagOxServer.Application.Services.Auth.Users.Validator;
using ShagOxServer.Application.Services.Baskets.Core.Validator;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.Core.Update;
public class BasketUpdateService
    : IBasketUpdateService
{
    private readonly IRepository<Basket> _basketRepository;
    private readonly BasketValidator _basketValidator;

    private readonly UserValidator _userValidator;

    private readonly IUnitOfWork _unitOfWork;


    public BasketUpdateService(
        IRepository<Basket> basketRepository,
        BasketValidator basketValidator,
        UserValidator userValidator,
        IUnitOfWork unitOfWork)
    {
        _basketRepository = basketRepository;
        _basketValidator = basketValidator;
        _userValidator = userValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int basketId,
        BasketUpdateRequest request)
    {
        var basket = await _basketValidator
            .GetByIdAsync(basketId);
        if (!basket.IsSuccess)
            return Result<UpdateResponse>.Fail(basket.Error);

        var validation = await
             ValidateUpdatesAsync(request);
        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);

        var updatedCount = BasketUpdater
            .ApplyUpdates(basket.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _basketRepository.Update(basket.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        BasketUpdateRequest request)
    {
        if (!request.UserId.HasValue)
            return Result<bool>.Success(true);

        var userExists = await _userValidator
                .ExistsByIdAsync(request.UserId.Value);

        if (!userExists.IsSuccess)
            return Result<bool>.Fail(userExists.Error);


        var basketUserExists = await _basketValidator
            .NotExistsByUserAsync(request.UserId.Value);

        if (!basketUserExists.IsSuccess)
            return Result<bool>.Fail(basketUserExists.Error);

        return Result<bool>.Success(true);
    }
}