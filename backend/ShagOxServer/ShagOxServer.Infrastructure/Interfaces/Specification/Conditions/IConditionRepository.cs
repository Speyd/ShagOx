using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Interfaces.Specification.Conditions;
public interface IConditionRepository
{
    Task<Condition?> GetByIdAsync(int id);

    Task AddAsync(Condition condition);

    Task<bool> UpdateAsync(Condition condition);

    Task DeleteAsync(Condition condition);
}
