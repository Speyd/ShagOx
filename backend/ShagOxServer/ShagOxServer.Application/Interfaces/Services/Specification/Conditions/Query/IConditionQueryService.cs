using ShagOxServer.Application.DTOs.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Query;
public interface IConditionQueryService
    : IQueryService<ConditionDto, Condition, ConditionSearchFilter>
{
    Task<Result<ConditionDto>> GetByCodeAsync(
        string code);
}