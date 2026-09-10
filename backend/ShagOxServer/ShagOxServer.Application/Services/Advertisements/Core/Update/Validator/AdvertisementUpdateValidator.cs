using ShagOxServer.Application.DTOs.Advertisements.Core.Update;
using ShagOxServer.Application.Services.Auth.Users.Validator;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Core.Update.Validator;
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
                .ExistsByIdAsync(request.BuyerId.Value);

            if (!buyer.IsSuccess)
                return buyer;
        }


        if (request.CurrencyId is not null)
        {
            var currency = await _currencyValidator
                .ExistsByIdAsync(request.CurrencyId.Value);

            if (!currency.IsSuccess)
                return currency;
        }


        if (request.ConditionId is not null)
        {
            var condition = await _conditionValidator
                .ExistsByIdAsync(request.ConditionId.Value);

            if (!condition.IsSuccess)
                return condition;
        }


        if (request.CategoryId is not null)
        {
            var category = await _categoryValidator
                .ExistsByIdAsync(request.CategoryId.Value);

            if (!category.IsSuccess)
                return category;
        }


        return Result<bool>.Success(true);
    }
}