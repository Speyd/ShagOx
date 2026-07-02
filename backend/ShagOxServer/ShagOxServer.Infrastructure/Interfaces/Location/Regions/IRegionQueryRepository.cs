using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Interfaces.Location.Regions;
public interface IRegionQueryRepository
{
    Task<Region?> GetByIdAsync(int id);

    Task<Region?> GetByNameAsync(string name);

    Task<List<Region>> SearchByName(
       string name,
       PaginationParams pagination);
}
