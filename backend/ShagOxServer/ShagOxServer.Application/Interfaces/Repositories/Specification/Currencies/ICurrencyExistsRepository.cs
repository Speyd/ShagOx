using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
public interface ICurrencyExistsRepository
    : IExistsRepository<Currency>
{
    Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsBySymbolAsync(
        string symbol,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
