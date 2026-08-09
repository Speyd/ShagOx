using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
public interface ICurrencyExistsRepository
    : IExistsRepository<Currency>
{
    Task<bool> ExistsByCodeAsync(string code);

    Task<bool> ExistsBySymbolAsync(string symbol);

    Task<bool> ExistsByNameAsync(string name);
}