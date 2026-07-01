namespace ShagOxServer.Infrastructure.Interfaces.Specification.Conditions;
public interface IConditionExistsRepository
{
    Task<bool> ExistsByNameAsync(string name);
}
