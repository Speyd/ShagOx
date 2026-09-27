using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
public interface IConditionQueryRepository
    : IQueryRepository<Condition, ConditionSearchFilter>
{
    Task<Condition?> GetByCodeAsync(
        string code);
}