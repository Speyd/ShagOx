using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Interfaces.Location.Regions;
public interface IRegionQueryRepository
{
    Task<Region?> GetByIdAsync(int id);

    Task<Region?> GetByNameAsync(string name);

    Task<List<Region>> Search(
       RegionSearchFilter filter,
       PaginationParams pagination);
}
