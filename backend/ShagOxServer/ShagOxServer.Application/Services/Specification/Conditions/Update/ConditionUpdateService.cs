using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Conditions.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Update;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Update;
public class ConditionUpdateService 
    : IConditionUpdateService
{
    private readonly IRepository<Condition> _conditionRepository;
    private readonly ConditionValidator _conditionValidator;

    private readonly IUnitOfWork _unitOfWork;


    public ConditionUpdateService(
        IRepository<Condition> conditionRepository,
        ConditionValidator conditionValidator,
        IUnitOfWork unitOfWork)
    {
        _conditionRepository = conditionRepository;
        _conditionValidator = conditionValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int conditionId, 
        ConditionUpdateRequest request)
    {
        var condition = await _conditionValidator
            .GetByIdAsync(conditionId);

        if (!condition.IsSuccess)
            return Result<UpdateResponse>.Fail(condition.Error );


        var validation = await
             ValidateUpdatesAsync(condition.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = ConditionUpdater
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
        Condition сondition,
        ConditionUpdateRequest request)
    {
        if (request.Code is not null &&
            request.Code != сondition.Code)
        {
            var nameValidation = await _conditionValidator
                .NotExistsByCodeAsync(request.Code);

            if (!nameValidation.IsSuccess)
                Result<CreateResponse>.Fail(nameValidation.Error);
        }

        return Result<bool>.Success(true);
    }
}