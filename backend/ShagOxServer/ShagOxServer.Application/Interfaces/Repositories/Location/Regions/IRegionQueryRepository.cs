using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
public interface IRegionQueryRepository
    : IQueryRepository<Region>
{
    Task<Region?> GetByCodeAsync(string code);

    Task<PagedResult<Region>> Search(
       RegionSearchFilter filter,
       PaginationParams pagination);
}