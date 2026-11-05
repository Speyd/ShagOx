using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
public interface IRegionExistsRepository
    : IExistsRepository<Region>
{
    Task<bool> ExistsByCodeAsync(
        string? code,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
