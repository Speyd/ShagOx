using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Interfaces.Specification.Conditions;
public interface IConditionQueryRepository
{
    Task<Condition?> GetByIdAsync(int id);

    Task<Condition?> GetByNameAsync(string name);

    Task<List<Condition>> SearchByName(
        string name,
        int page,
        int pageSize);
}
