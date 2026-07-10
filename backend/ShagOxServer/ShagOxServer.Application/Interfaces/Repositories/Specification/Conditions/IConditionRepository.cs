using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
public interface IConditionRepository
{
    Task<Condition?> GetByIdAsync(int id);

    void AddAsync(Condition condition);

    bool UpdateAsync(Condition condition);

    void DeleteAsync(Condition condition);
}