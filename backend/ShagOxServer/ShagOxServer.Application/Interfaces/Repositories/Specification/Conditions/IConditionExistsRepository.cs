namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
public interface IConditionExistsRepository
{
    Task<bool> ExistsByIdAsync(int id);

    Task<bool> ExistsByNameAsync(string name);
}