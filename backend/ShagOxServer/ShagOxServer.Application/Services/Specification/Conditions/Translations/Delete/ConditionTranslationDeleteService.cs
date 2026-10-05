using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Translations.Query.Mapping;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Translations;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Validator;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Translations.Delete;
public class ConditionTranslationDeleteService
    : IConditionTranslationDeleteService
{
    private readonly IRepository<ConditionTranslation> _conditionRepository;
    private readonly ConditionTranslationValidator _conditionValidator;

    private readonly ConditionTranslationInvalidationService _transInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ConditionTranslationDeleteService> _logger;


    public ConditionTranslationDeleteService(
        IRepository<ConditionTranslation> conditionRepository,
        ConditionTranslationValidator conditionValidator,
        ConditionTranslationInvalidationService transInvalid,
        IUnitOfWork unitOfWork,
        ILogger<ConditionTranslationDeleteService> logger)
    {
        _conditionRepository = conditionRepository;
        _conditionValidator = conditionValidator;
        _transInvalid = transInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        var condition = await _conditionValidator
            .GetByIdAsync(id);

        if (!condition.IsSuccess)
        {
            return Result<DeleteResponse>
                .Fail(condition.Error);
        }

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _conditionRepository.Delete(condition.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete condition translation. Id: {Id}",
                id);

            return Result<DeleteResponse>
                 .Fail(EntityErrorResources.ConditionTranslationDeleteFailed);
        }

        await _transInvalid.InvalidateUpdateAsync(
            BaseTranslationCacheMapper.ToInfo(condition.Value!));

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               condition.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}