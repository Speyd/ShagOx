using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
public interface IRegionExistsRepository
    : IExistsRepository<Region>
{
    Task<bool> ExistsByCodeAsync(string? code);
}