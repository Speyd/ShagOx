using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Specification.Conditions;

namespace ShagOxServer.Application.Interfaces.Specification.Conditions.Query;
public interface IConditionQueryService
{
    Task<Result<ConditionDto>> GetByIdAsync(int id);

    Task<Result<ConditionDto>> GetByNameAsync(string name);
}
