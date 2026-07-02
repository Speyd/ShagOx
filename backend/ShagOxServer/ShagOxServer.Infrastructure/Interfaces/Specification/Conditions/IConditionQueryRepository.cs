using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Interfaces.Specification.Conditions;
public interface IConditionQueryRepository
{
    Task<Condition?> GetByIdAsync(int id);

    Task<Condition?> GetByNameAsync(string name);

    Task<List<Condition>> SearchByName(
        string name,
        PaginationParams pagination);
}
