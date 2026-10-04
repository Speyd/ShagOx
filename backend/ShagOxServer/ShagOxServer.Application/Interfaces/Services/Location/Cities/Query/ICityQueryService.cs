using ShagOxServer.Application.DTOs.Location.Cities.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Query;
public interface ICityQueryService
    : ITranslatableQueryService<CityDto,
        City, 
        CitySearchFilter>
{
    Task<Result<PagedResult<CityDto>>> GetByRegionAsync(
        long regionId,
        PaginationParams pagination);
}