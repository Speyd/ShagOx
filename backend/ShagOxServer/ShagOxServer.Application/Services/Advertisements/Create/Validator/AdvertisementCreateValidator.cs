using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Create.Validator;
public class AdvertisementCreateValidator
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IConditionRepository _conditionRepository;


    public AdvertisementCreateValidator(
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
       AdvertisementCreateRequest request)
    {
        var currency = await _currencyRepository.GetByIdAsync(request.CurrencyId);
        if (currency is null)
            return Result<bool>
                .NotFound("Currency");

        var condition = await _conditionRepository.GetByIdAsync(request.ConditionId);
        if (condition is null)
            return Result<bool>
                .NotFound("Condition");

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null)
            return Result<bool>
                .NotFound("Category");

        return Result<bool>
            .Success(true);
    }
}