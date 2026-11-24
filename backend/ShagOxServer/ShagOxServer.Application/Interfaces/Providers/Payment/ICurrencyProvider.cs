using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Providers.Payment;
public interface ICurrencyProvider
{
    Task<Currency?> GetCurrencyAsync(
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Currency?> GetBaseCurrencyAsync(
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
