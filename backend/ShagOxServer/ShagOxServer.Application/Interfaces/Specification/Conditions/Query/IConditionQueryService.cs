using ShagOxServer.Application.DTOs.Specification.Conditions;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Paginations;

namespace ShagOxServer.Application.Interfaces.Specification.Conditions.Query;
public interface IConditionQueryService
{
    Task<Result<ConditionDto>> GetByIdAsync(int id);

    Task<Result<ConditionDto>> GetByNameAsync(string name);

    Task<Result<List<ConditionDto>>> SearchByName(
       string name,
       PaginationParams pagination);
}
