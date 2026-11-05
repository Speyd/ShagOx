using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Providers;
public interface ICurrencyProvider
{
    Task<Currency?> GetCurrencyAsync(
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
