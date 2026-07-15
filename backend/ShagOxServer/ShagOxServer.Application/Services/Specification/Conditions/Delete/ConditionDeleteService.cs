using ShagOxServer.Application.DTOs.Specification.Conditions.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Delete;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Delete;
public class ConditionDeleteService : IConditionDeleteService
{
    private readonly IConditionRepository _conditionRepository;
    private readonly ConditionValidator _conditionValidator;


    private readonly IUnitOfWork _unitOfWork;


    public ConditionDeleteService(
        IConditionRepository conditionRepository,
        ConditionValidator conditionValidator,
        IUnitOfWork unitOfWork)
    {
        _conditionRepository = conditionRepository;
        _conditionValidator = conditionValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<ConditionDeleteResponse>> DeleteAsync(
        int id)
    {
        var condition = await _conditionValidator.GetByIdAsync(id);
        if (!condition.IsSuccess)
            return Result<ConditionDeleteResponse>.Fail(condition.Error ?? "");

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _conditionRepository.Delete(condition.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }


        return Result<ConditionDeleteResponse>.Success(
           new ConditionDeleteResponse(
               condition.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}