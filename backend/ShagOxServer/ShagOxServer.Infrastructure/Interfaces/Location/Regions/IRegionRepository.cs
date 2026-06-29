using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Infrastructure.Interfaces.Location.Regions;
internal interface IRegionRepository
{
    Task<Region?> GetByIdAsync(int id);

    Task<Region?> GetByNameAsync(string name);


    Task<bool> ExistsAsync(string name);


    Task AddAsync(Region region);

    Task<bool> UpdateAsync(Region region);

    Task DeleteAsync(Region region);
}
