using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Update;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Validator;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Translations.Update;
public class ConditionTranslationUpdateService
    : BaseTranslationSerivce<Condition, ConditionTranslation>,
    IConditionTranslationUpdateService
{
    private readonly IRepository<ConditionTranslation> _conditionRepository;
    private readonly ConditionTranslationValidator _conditionTranslationValidator;


    private readonly IUnitOfWork _unitOfWork;


    public ConditionTranslationUpdateService(
        IRepository<ConditionTranslation> conditionRepository,
        ConditionTranslationValidator conditionTranslationValidator,
        ConditionValidator conditionValidator,
        IUnitOfWork unitOfWork
    ) : base(conditionValidator, conditionTranslationValidator)
    {
        _conditionRepository = conditionRepository;
        _conditionTranslationValidator = conditionTranslationValidator;
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
}