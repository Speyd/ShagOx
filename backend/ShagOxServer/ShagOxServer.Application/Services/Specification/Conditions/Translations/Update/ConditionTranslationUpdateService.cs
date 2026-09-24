using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Validator;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Translations.Update;
public class ConditionTranslationUpdateService
    : BaseTranslationUpdateSerivce<Condition, ConditionTranslation>,
    IConditionTranslationUpdateService
{
    private readonly IRepository<ConditionTranslation> _conditionRepository;
    private readonly ConditionTranslationValidator _conditionTranslationValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ConditionTranslationUpdateService> _logger;


    public ConditionTranslationUpdateService(
        IRepository<ConditionTranslation> conditionRepository,
        ConditionTranslationValidator conditionTranslationValidator,
        ConditionValidator conditionValidator,
        IUnitOfWork unitOfWork,
        ILogger<ConditionTranslationUpdateService> logger
    ) : base(conditionValidator, conditionTranslationValidator)
    {
        _conditionRepository = conditionRepository;
        _conditionTranslationValidator = conditionTranslationValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
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
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update condition translation. Id: {Id}",
                conditionTranslationId);

            return Result<UpdateResponse>
                 .Fail(EntityError.ConditionTranslationUpdateFailed);
        }

        return Result<UpdateResponse>.Success(result);
    }
}