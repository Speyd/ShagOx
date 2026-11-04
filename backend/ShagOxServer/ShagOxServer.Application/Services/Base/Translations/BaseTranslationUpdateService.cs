using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Requests.Translations;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Base.Update;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Base;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Base.Translations;
public abstract class BaseTranslationUpdateService<TObject, TTranslation>
    : BaseUpdateService<TTranslation>
    where TObject : BaseEntity
    where TTranslation : BaseTranslation<TObject>
{
    protected readonly BaseValidator<TObject> _objectValidator;
    protected readonly BaseTranslationValidator<TObject, TTranslation> _translationValidator;


    public BaseTranslationUpdateService(
        BaseValidator<TObject> objectValidator,
        ISoftDeleteService<TTranslation, DeleteResponse> deleteService,
        BaseTranslationValidator<TObject, TTranslation> translationValidator,
        ILogger<BaseTranslationUpdateService<TObject, TTranslation>> logger,
        IUnitOfWork unitOfWork
    )
        : base(deleteService, translationValidator, logger, unitOfWork)
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
