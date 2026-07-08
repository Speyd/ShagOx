namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
public interface IConditionExistsRepository
{
    Task<bool> ExistsByNameAsync(string name);
}
