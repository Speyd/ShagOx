using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
public interface IStatusExistsRepository
    : IExistsRepository<Status>
{
    Task<bool> ExistsByCodeAsync(
        AdvertisementStatus code,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
