using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
public interface IConditionQueryRepository
    : IQueryRepository<Condition>
{
    Task<Condition?> GetByCodeAsync(string code);

    Task<PagedResult<Condition>> Search(
        ConditionSearchFilter filter,
        PaginationParams pagination);
}