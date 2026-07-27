using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
public interface IConditionQueryRepository
{
    Task<Condition?> GetByIdAsync(int id);

    Task<PagedResult<Condition>> GetPagedAsync(
        PaginationParams pagination);

    Task<Condition?> GetByNameAsync(string name);

    Task<PagedResult<Condition>> Search(
        ConditionSearchFilter filter,
        PaginationParams pagination);
}