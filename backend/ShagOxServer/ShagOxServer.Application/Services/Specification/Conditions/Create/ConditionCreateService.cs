using ShagOxServer.Application.DTOs.Specification.Conditions.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Conditions.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Create;
public class ConditionCreateService : IConditionCreateService
{
    private readonly IConditionRepository _repository;

    private readonly IUnitOfWork _unitOfWork;


    public ConditionCreateService(
        IConditionRepository conditionRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = conditionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ConditionCreateResponse>> CreateConditionAsync(
        ConditionCreateRequest request)
    {

        var condition = ConditionCreater.CreateCondition(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Add(condition);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        var response = new ConditionCreateResponse(
            condition.Id,
            DateTime.UtcNow
        );

        return Result<ConditionCreateResponse>.Success(response);
    }
}