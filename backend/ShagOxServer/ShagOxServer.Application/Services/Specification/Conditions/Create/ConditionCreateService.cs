using ShagOxServer.Application.DTOs.Specification.Conditions.Create;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Conditions.Create;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Create;
public class ConditionCreateService : IConditionCreateService
{
    private readonly IConditionRepository _repository;

    public ConditionCreateService(
        IConditionRepository conditionRepository)
    {
        _repository = conditionRepository;
    }

    public async Task<Result<ConditionCreateResponse>> CreateConditionAsync(
        ConditionCreateRequest request)
    {
        var condition = CreateCondition(request);

        await _repository.AddAsync(condition);

        var response = new ConditionCreateResponse(
            condition.Id,
            DateTime.UtcNow
        );

        return Result<ConditionCreateResponse>.Success(response);
    }

    private Condition CreateCondition(
       ConditionCreateRequest request)
    {
        return new Condition
        {
            Name = request.Name,
        };
    }
}
