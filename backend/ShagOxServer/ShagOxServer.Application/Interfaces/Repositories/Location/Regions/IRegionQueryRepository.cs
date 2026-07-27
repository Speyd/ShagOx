using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
public interface IRegionQueryRepository
{
    Task<Region?> GetByIdAsync(int id);

    Task<List<Region>> GetPagedAsync(
        PaginationParams pagination);

    Task<Region?> GetByNameAsync(string name);

    Task<List<Region>> Search(
       RegionSearchFilter filter,
       PaginationParams pagination);
}
