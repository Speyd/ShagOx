using ShagOxServer.Application.DTOs.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Specification.Conditions.Mapping;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Specification.Conditions.Query;
public class ConditionQueryService 
    : BaseQueryService<
        ConditionDto,
        Condition,
        ConditionSearchFilter
        >,
    IConditionQueryService
{
    private readonly IConditionQueryRepository _conditionQueryRepository;


    public ConditionQueryService(
        IConditionQueryRepository conditionQueryRepository
    )
        : base(conditionQueryRepository)
    {
        _conditionQueryRepository = conditionQueryRepository;
    }


    protected override ConditionDto ApplyMapper(
        Condition entity)
    {
        return ConditionMapper.ToDto(entity);
    }

    public async Task<Result<ConditionDto>> GetByCodeAsync(
        string code)
    {
        var condition = await _conditionQueryRepository
            .GetByCodeAsync(code);

        return condition.ToResult(ConditionMapper.ToDto);
    }
}