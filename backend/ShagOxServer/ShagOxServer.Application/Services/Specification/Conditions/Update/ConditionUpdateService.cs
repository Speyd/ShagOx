using ShagOxServer.Application.DTOs.Specification.Conditions.Create;
using ShagOxServer.Application.DTOs.Specification.Conditions.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Update;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Update;
public class ConditionUpdateService : IConditionUpdateService
{
    private readonly IConditionRepository _conditionRepository;
    private readonly ConditionValidator _conditionValidator;

    private readonly IUnitOfWork _unitOfWork;


    public ConditionUpdateService(
        IConditionRepository conditionRepository,
        ConditionValidator conditionValidator,
        IUnitOfWork unitOfWork)
    {
        _conditionRepository = conditionRepository;
        _conditionValidator = conditionValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<ConditionUpdateResponse>> UpdateAsync(
        int conditionId, 
        ConditionUpdateRequest request)
    {
        var condition = await _conditionValidator
            .GetByIdAsync(conditionId);

        if (!condition.IsSuccess)
            return Result<ConditionUpdateResponse>.Fail(condition.Error ?? "");

        if (request.Name is not null)
        {
            var validationName = await _conditionValidator
                .NotExistsByNameAsync(request.Name);

            if (!validationName.IsSuccess)
                Result<ConditionCreateResponse>.Fail(validationName.Error ?? "");
        }

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
            _conditionRepository.Update(condition.Value!);

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