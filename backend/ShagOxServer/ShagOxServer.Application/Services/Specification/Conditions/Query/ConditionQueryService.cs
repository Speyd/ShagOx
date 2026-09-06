using ShagOxServer.Application.DTOs.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Query;
using ShagOxServer.Application.Services.Specification.Conditions.Mapping;
using ShagOxServer.Domain.Filters.Specification.Conditions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Specification.Conditions.Query;
public class ConditionQueryService 
    : IConditionQueryService
{
    private readonly IConditionQueryRepository _conditionQueryRepository;


    public ConditionQueryService(
        IConditionQueryRepository conditionQueryRepository)
    {
        _conditionQueryRepository = conditionQueryRepository;
    }


    public async Task<Result<ConditionDto>> GetByIdAsync(
        int id)
    {
        var condition = await _conditionQueryRepository
            .GetByIdAsync(id);

        return condition.ToResult(ConditionMapper.ToDto);
    }

    public async Task<Result<PagedResult<ConditionDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var conditions = await _conditionQueryRepository
            .GetPagedAsync(pagination);

        return conditions.ToResultPaged(ConditionMapper.ToDto);
    }

    public async Task<Result<ConditionDto>> GetByCodeAsync(
        string code)
    {
        var condition = await _conditionQueryRepository
            .GetByCodeAsync(code);

        return condition.ToResult(ConditionMapper.ToDto);
    }

    public async Task<Result<PagedResult<ConditionDto>>> Search(
      ConditionSearchFilter filter,
	  PaginationParams pagination)
    {
        var conditions = await _conditionQueryRepository
            .Search(filter, pagination);

        return conditions.ToResultPaged(ConditionMapper.ToDto);
    }
}