using ShagOxServer.Application.DTOs.Specification.Conditions.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Conditions.Update;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Update;
public class ConditionUpdateService : IConditionUpdateService
{
    private readonly IConditionRepository _repository;
    private readonly ConditionValidator _validator;

    private readonly IUnitOfWork _unitOfWork;

    public ConditionUpdateService(
        IConditionRepository conditionRepository,
        ConditionValidator validator,
        IUnitOfWork unitOfWork)
    {
        _repository = conditionRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ConditionUpdateResponse>> UpdateConditionAsync(
        int conditionId, 
        ConditionUpdateRequest request)
    {
        var condition = await _validator.GetConditionValidator(conditionId);
        if (!condition.IsSuccess)
            return Result<ConditionUpdateResponse>.Fail(condition.Error ?? "");

        var updatedCount = ConditionUpdater.ApplyUpdates(condition.Value!, request);
        var result = new ConditionUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<ConditionUpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Update(condition.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }


        return Result<ConditionUpdateResponse>.Success(result);
    }
}