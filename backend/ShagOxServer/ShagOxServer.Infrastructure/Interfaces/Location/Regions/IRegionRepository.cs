using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Infrastructure.Interfaces.Location.Regions;
public interface IRegionRepository
{
    Task<Region?> GetByIdAsync(int id);

    Task AddAsync(Region region);

    Task<bool> UpdateAsync(Region region);

    Task DeleteAsync(Region region);
}
