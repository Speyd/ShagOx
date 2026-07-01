using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Query;
using ShagOxServer.Application.Services.Specification.Conditions.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Specification.Conditions;

namespace ShagOxServer.Application.Services.Specification.Conditions.Query;
public class ConditionQueryService : IConditionQueryService
{
    private readonly IConditionRepository _repository;

    public ConditionQueryService(
        IConditionRepository conditionRepository)
    {
        _repository = conditionRepository;
    }

    public async Task<Result<ConditionDto>> GetByIdAsync(int id)
    {
        var condition = await _repository.GetByIdAsync(id);

        return condition.ToResult(ConditionMapper.ToDto);
    }

    public async Task<Result<ConditionDto>> GetByNameAsync(string name)
    {
        var condition = await _repository.GetByNameAsync(name);

        return condition.ToResult(ConditionMapper.ToDto);
    }
}
