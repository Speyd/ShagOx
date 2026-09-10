using ShagOxServer.Application.DTOs.Advertisements.Core.Create;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Core.Create.Validator;
public class AdvertisementCreateValidator
{
    private readonly CurrencyValidator _currencyValidator;
    private readonly CategoryValidator _categoryValidator;
    private readonly ConditionValidator _conditionValidator;


    public AdvertisementCreateValidator(
        CurrencyValidator currencyValidator,
        CategoryValidator categoryValidator,
        ConditionValidator conditionValidator)
    {
        _currencyValidator = currencyValidator;
        _categoryValidator = categoryValidator;
        _conditionValidator = conditionValidator;
    }


    public async Task<Result<bool>> ValidateAsync(
       AdvertisementCreateRequest request)
    {
        var currency = await _currencyValidator
            .ExistsByIdAsync(request.CurrencyId);

        if (!currency.IsSuccess)
            return currency;


        var condition = await _conditionValidator
            .ExistsByIdAsync(request.ConditionId);

        if (!condition.IsSuccess)
            return condition;


        var category = await _categoryValidator
            .ExistsByIdAsync(request.CategoryId);

        if (!category.IsSuccess)
            return category;

        return Result<bool>
            .Success(true);
    }
}