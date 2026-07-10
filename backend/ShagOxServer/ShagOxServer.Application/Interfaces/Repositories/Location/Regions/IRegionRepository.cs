using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
public interface IRegionRepository
{
    Task<Region?> GetByIdAsync(int id);

    void Add(Region region);

    bool Update(Region region);

    void Delete(Region region);
}
