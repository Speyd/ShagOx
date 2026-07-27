using ShagOxServer.Application.DTOs.Specification.Conditions;
using ShagOxServer.Domain.Filters.Specification.Conditions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Query;
public interface IConditionQueryService
{
    Task<Result<ConditionDto>> GetByIdAsync(int id);

    Task<Result<List<ConditionDto>>> GetPagedAsync(
        PaginationParams pagination);

    Task<Result<ConditionDto>> GetByNameAsync(string name);

    Task<Result<List<ConditionDto>>> Search(
       ConditionSearchFilter filter,
       PaginationParams pagination);
}