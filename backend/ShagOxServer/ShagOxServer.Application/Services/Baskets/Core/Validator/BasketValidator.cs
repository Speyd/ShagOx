using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.Core.Validator;
public class BasketValidator
    : BaseValidator<Basket>
{
    private readonly IBasketExistsRepository _basketExistsRepository;

    public BasketValidator(
        IRepository<Basket> basketRepository,
        IBasketExistsRepository basketExistsRepository
    ) : base(basketRepository, basketExistsRepository)
    {
        _basketExistsRepository = basketExistsRepository;
    }


    public async Task<Result<bool>> ExistsByUserAsync(
        int userId)
    {
        if (!await _basketExistsRepository
                .ExistsByUserAsync(userId))
        {
            return Result<bool>
                .NotFound(typeof(Basket));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByUserAsync(
        int userId)
    {
        if (await _basketExistsRepository
                .ExistsByUserAsync(userId))
        {
            return Result<bool>
                .AlreadyExists(typeof(Basket));
        }

        return Result<bool>.Success(true);
    }
}