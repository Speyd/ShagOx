using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.Application.Services.Users.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Update.Validator;

public class AdvertisementUpdateValidator
{
    private readonly UserValidator _userValidator;
    private readonly CurrencyValidator _currencyValidator;
    private readonly CategoryValidator _categoryValidator;
    private readonly ConditionValidator _conditionValidator;


    public AdvertisementUpdateValidator(
        UserValidator userValidator,
        CurrencyValidator currencyValidator,
        CategoryValidator categoryValidator,
        ConditionValidator conditionValidator)
    {
        _userValidator = userValidator;
        _currencyValidator = currencyValidator;
        _categoryValidator = categoryValidator;
        _conditionValidator = conditionValidator;
    }


    public async Task<Result<bool>> ValidateAsync(
        AdvertisementUpdateRequest request)
    {
        if (request.BuyerId is not null)
        {
            var buyer = await _userValidator
                .ExistsUserValidator(request.BuyerId.Value);

            if (!buyer.IsSuccess)
                return buyer;
        }


        if (request.CurrencyId is not null)
        {
            var currency = await _currencyValidator
                .ExistsCurrencyValidator(request.CurrencyId.Value);

            if (!currency.IsSuccess)
                return currency;
        }


        if (request.ConditionId is not null)
        {
            var condition = await _conditionValidator
                .ExistsByIdValidator(request.ConditionId.Value);

            if (!condition.IsSuccess)
                return condition;
        }


        if (request.CategoryId is not null)
        {
            var category = await _categoryValidator
                .ExistsCategoryValidator(request.CategoryId.Value);

            if (!category.IsSuccess)
                return category;
        }


        return Result<bool>.Success(true);
    }
}