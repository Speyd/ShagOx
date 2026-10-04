using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification;
using ShagOxServer.Application.Services.Specification.Conditions.Create;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Delete;
public class ConditionDeleteService 
    : IConditionDeleteService
{
    private readonly IRepository<Condition> _conditionRepository;
    private readonly ConditionValidator _conditionValidator;

    private readonly ConditionInvalidationService _conditionInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ConditionCreateService> _logger;


    public ConditionDeleteService(
        IRepository<Condition> conditionRepository,
        ConditionValidator conditionValidator,
        ConditionInvalidationService conditionInvalid,
        IUnitOfWork unitOfWork,
        ILogger<ConditionCreateService> logger)
    {
        _conditionRepository = conditionRepository;
        _conditionValidator = conditionValidator;
        _conditionInvalid = conditionInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        var condition = await _conditionValidator.GetByIdAsync(id);
        if (!condition.IsSuccess)
            return Result<DeleteResponse>.Fail(condition.Error);

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
                "Failed to delete condition. Id: {Id}",
                id);

            return Result<DeleteResponse>
                 .Fail(EntityErrorResources.ConditionDeleteFailed);
        }

        await _conditionInvalid
            .InvalidateDeleteAsync(id);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               condition.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}