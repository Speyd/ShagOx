using ShagOxServer.Application.DTOs.Location.Cities;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Query;
public interface ICityQueryService
    : IQueryService<CityDto, City, CitySearchFilter>
{
    Task<Result<CityDto>> GetByCodeAsync(
        string code);

    Task<Result<PagedResult<CityDto>>> GetByRegionAsync(
        long regionId,
        PaginationParams pagination);
}