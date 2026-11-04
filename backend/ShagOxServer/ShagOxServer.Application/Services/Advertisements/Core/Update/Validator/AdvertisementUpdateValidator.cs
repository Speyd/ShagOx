using ShagOxServer.Application.DTOs.Advertisements.Core.Update;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
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


    public Task<Result<bool>> ValidateUpdateConflictsAsync(
        AdvertisementUpdateRequest request)
    {
        return ValidateReferencesAsync(
            request.BuyerId,
            request.CurrencyId,
            request.ConditionId,
            request.CategoryId);
    }

    public Task<Result<bool>> ValidateUpdateConflictsAsync(
        Advertisement advert)
    {
        return ValidateReferencesAsync(
            advert.BuyerId,
            advert.CurrencyId,
            advert.ConditionId,
            advert.CategoryId);
    }

    private async Task<Result<bool>> ValidateReferencesAsync(
        long? buyerId,
        long? currencyId,
        long? conditionId,
        long? categoryId)
    {
        if (buyerId.HasValue)
        {
            var result = await _userValidator
                .ExistsByIdAsync(buyerId.Value);
            if (!result.IsSuccess)
                return result;
        }

        if (currencyId.HasValue)
        {
            var result = await _currencyValidator
                .ExistsByIdAsync(currencyId.Value);
            if (!result.IsSuccess)
                return result;
        }

        if (conditionId.HasValue)
        {
            var result = await _conditionValidator
                .ExistsByIdAsync(conditionId.Value);
            if (!result.IsSuccess)
                return result;
        }

        if (categoryId.HasValue)
        {
            var result = await _categoryValidator
                .ExistsByIdAsync(categoryId.Value);
            if (!result.IsSuccess)
                return result;
        }

        return Result<bool>.Success(true);
    }
}
