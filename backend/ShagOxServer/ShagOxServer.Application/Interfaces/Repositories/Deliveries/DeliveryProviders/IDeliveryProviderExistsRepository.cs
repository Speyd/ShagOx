using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Deliveries;

namespace ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders;
public interface IDeliveryProviderExistsRepository
    : IExistsRepository<DeliveryProvider>
{
    Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
