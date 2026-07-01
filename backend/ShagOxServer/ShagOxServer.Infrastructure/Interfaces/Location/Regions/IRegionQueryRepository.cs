using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Infrastructure.Interfaces.Location.Regions;
public interface IRegionQueryRepository
{
    Task<Region?> GetByIdAsync(int id);

    Task<Region?> GetByNameAsync(string name);

    Task<List<Region>> SearchByName(
       string name,
       int page,
       int pageSize);
}
