using ShagOxServer.Application.DTOs.Common.Requests.Translations;
using ShagOxServer.Domain.Base;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Base.Translations;
public abstract class BaseTranslationSerivce<TObject, TTranslation>
    where TObject : BaseEntity
    where TTranslation : BaseTranslation<TObject>
{
    protected readonly BaseValidator<TObject> _objectValidator;
    protected readonly BaseTranslationValidator<TTranslation> _translationValidator;


    public BaseTranslationSerivce(
        BaseValidator<TObject> objectValidator,
        BaseTranslationValidator<TTranslation> translationValidator
        )
    {
        _objectValidator = objectValidator;
        _translationValidator = translationValidator;
    }

    protected async Task<Result<bool>> ValidateUpdatesAsync(
        TTranslation translation,
        TranslationUpdateRequest request)
    {
        if (request.TranslatableId is not null &&
           request.TranslatableId != translation.TranslatableId)
        {
            var validator = await _objectValidator
                .ExistsByIdAsync(request.TranslatableId.Value);

            if (!validator.IsSuccess)
                return Result<bool>.Fail(validator.Error);
        }

        if (request.Language is not null)
        {
            if ((request.TranslatableId is not null &&
                request.TranslatableId != translation.TranslatableId) ||
                request.Language != translation.Language)
            {
                var validator = await _translationValidator
                    .ExistsAsync(
                        request.TranslatableId ?? translation.TranslatableId,
                        request.Language);

                if (!validator.IsSuccess)
                    return Result<bool>.Fail(validator.Error);
            }
        }

        return Result<bool>.Success(true);
    }
}