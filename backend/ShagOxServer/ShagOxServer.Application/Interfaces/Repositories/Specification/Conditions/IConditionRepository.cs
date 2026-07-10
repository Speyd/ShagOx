using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
public interface IConditionRepository
{
    Task<Condition?> GetByIdAsync(int id);

    void Add(Condition condition);

    bool Update(Condition condition);

    void Delete(Condition condition);
}