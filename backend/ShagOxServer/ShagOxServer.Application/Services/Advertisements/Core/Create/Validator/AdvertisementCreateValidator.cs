using ShagOxServer.Application.DTOs.Advertisements.Core.Create;
using ShagOxServer.Application.Services.Advertisements.Statuses.Validator;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Core.Create.Validator;
public class AdvertisementCreateValidator
{
    private readonly UserValidator _userValidator;
    private readonly CurrencyValidator _currencyValidator;
    private readonly CategoryValidator _categoryValidator;
    private readonly ConditionValidator _conditionValidator;
    private readonly StatusValidator _statusValidator;


    public AdvertisementCreateValidator(
        UserValidator userValidator,
        CurrencyValidator currencyValidator,
        CategoryValidator categoryValidator,
        ConditionValidator conditionValidator,
        StatusValidator statusValidator)
    {
        _userValidator = userValidator;
        _currencyValidator = currencyValidator;
        _categoryValidator = categoryValidator;
        _conditionValidator = conditionValidator;
        _statusValidator = statusValidator;
    }


    public async Task<Result<bool>> ValidateAsync(
       AdvertisementCreateRequest request,
       long ownerId)
    {
        var user = await _userValidator
            .ExistsByIdAsync(ownerId);

        var status = await _statusValidator
            .ExistsByIdAsync(request.StatusId);

        if (!status.IsSuccess)
            return status;

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
