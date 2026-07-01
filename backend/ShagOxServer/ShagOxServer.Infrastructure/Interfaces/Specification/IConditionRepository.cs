using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Interfaces.Specification;
public interface IConditionRepository
{
    Task AddAsync(Condition condition);

    Task<bool> UpdateAsync(Condition condition);

    Task DeleteAsync(Condition condition);


    Task<Condition?> GetByIdAsync(int id);

    Task<Condition?> GetByNameAsync(string name);


    Task<bool> ExistsByNameAsync(string name);


}
