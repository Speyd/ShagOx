using ShagOxServer.Application.DTOs.Specification.Conditions;
using ShagOxServer.SharedKernel.Results;

namespace ShagOxServer.Application.Interfaces.Specification.Conditions.Query;
public interface IConditionQueryService
{
    Task<Result<ConditionDto>> GetByIdAsync(int id);

    Task<Result<ConditionDto>> GetByNameAsync(string name);

    Task<Result<List<ConditionDto>>> SearchByName(
       string name,
       int page,
       int pageSize);
}
