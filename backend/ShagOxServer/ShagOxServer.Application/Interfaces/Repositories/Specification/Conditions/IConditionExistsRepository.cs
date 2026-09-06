using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
public interface IConditionExistsRepository
    : IExistsRepository<Condition>
{
    Task<bool> ExistsByCodeAsync(string code);
}