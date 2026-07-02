using ShagOxServer.SharedKernel.Results;
using ShagOxServer.SharedKernel.Results.Extensions;
using ShagOxServer.Application.DTOs.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Query;
using ShagOxServer.Application.Services.Specification.Conditions.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Specification.Conditions;

namespace ShagOxServer.Application.Services.Specification.Conditions.Query;
public class ConditionQueryService : IConditionQueryService
{
    private readonly IConditionQueryRepository _repository;

    public ConditionQueryService(
        IConditionQueryRepository conditionRepository)
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

    public async Task<Result<List<ConditionDto>>> SearchByName(
      string name,
      int page,
      int pageSize)
    {
        var conditions = await _repository.SearchByName(name, page, pageSize);

        return conditions.ToResultList(ConditionMapper.ToDto);
    }
}
