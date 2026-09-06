using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Update;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Validator;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Translations.Update;
public class ConditionTranslationUpdateService
    : IConditionTranslationUpdateService
{
    private readonly IRepository<ConditionTranslation> _conditionRepository;
    private readonly ConditionTranslationValidator _conditionTranslationValidator;
    private readonly ConditionValidator _conditionValidator;


    private readonly IUnitOfWork _unitOfWork;


    public ConditionTranslationUpdateService(
        IRepository<ConditionTranslation> conditionRepository,
        ConditionTranslationValidator conditionTranslationValidator,
        ConditionValidator conditionValidator,
        IUnitOfWork unitOfWork)
    {
        _conditionRepository = conditionRepository;
        _conditionTranslationValidator = conditionTranslationValidator;
        _conditionValidator = conditionValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int conditionTranslationId,
        ConditionTranslationUpdateRequest request)
    {
        var condition = await _conditionTranslationValidator
            .GetByIdAsync(conditionTranslationId);

        if (!condition.IsSuccess)
            return Result<UpdateResponse>.Fail(condition.Error);


        var validation = await
             ValidateUpdatesAsync(condition.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = ConditionTranslationUpdater
            .ApplyUpdates(condition.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _conditionRepository.Update(condition.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        ConditionTranslation condition,
        ConditionTranslationUpdateRequest request)
    {
        if (request.ConditionId is not null &&
           request.ConditionId != condition.TranslatableId)
        {
            var validator = await _conditionValidator
                .ExistsByIdAsync(request.ConditionId.Value);

            if (!validator.IsSuccess)
                return Result<bool>.Fail(validator.Error);
        }

        if (request.Language is not null)
        {
            if ((request.ConditionId is not null &&
                request.ConditionId != condition.TranslatableId) ||
                request.Language != condition.Language)
            {
                var validator = await _conditionTranslationValidator
                    .ExistsAsync(
                        request.ConditionId ?? condition.TranslatableId,
                        request.Language);

                if (!validator.IsSuccess)
                    return Result<bool>.Fail(validator.Error);
            }
        }

        return Result<bool>.Success(true);
    }
}