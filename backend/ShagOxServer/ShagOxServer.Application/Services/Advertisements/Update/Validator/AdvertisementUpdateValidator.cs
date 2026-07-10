using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Update.Validator;

public class AdvertisementUpdateValidator
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IConditionRepository _conditionRepository;


    public AdvertisementUpdateValidator(
        IUserRepository userRepository,
        ICurrencyRepository currencyRepository,
        ICategoryRepository categoryRepository,
        IConditionRepository conditionRepository)
    {
        _userRepository = userRepository;
        _currencyRepository = currencyRepository;
        _categoryRepository = categoryRepository;
        _conditionRepository = conditionRepository;
    }


    public async Task<Result<bool>> ValidateAsync(
        AdvertisementUpdateRequest request)
    {
        if (request.BuyerId is not null)
        {
            var buyer = await _userRepository
                .GetByIdAsync(request.BuyerId.Value);

            if (buyer is null)
                return Result<bool>.NotFound("Buyer");
        }


        if (request.CurrencyId is not null)
        {
            var currency = await _currencyRepository
                .GetByIdAsync(request.CurrencyId.Value);

            if (currency is null)
                return Result<bool>.NotFound("Currency");
        }


        if (request.ConditionId is not null)
        {
            var condition = await _conditionRepository
                .GetByIdAsync(request.ConditionId.Value);

            if (condition is null)
                return Result<bool>.NotFound("Condition");
        }


        if (request.CategoryId is not null)
        {
            var category = await _categoryRepository
                .GetByIdAsync(request.CategoryId.Value);

            if (category is null)
                return Result<bool>.NotFound("Category");
        }


        return Result<bool>.Success(true);
    }
}