using ShagOxServer.Application.DTOs.Auth.Users;
using ShagOxServer.Application.DTOs.Location.Cities;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Query;
public interface ICityQueryService
    : IQueryService<CityDto>
{
    Task<Result<CityDto>> GetByNameAsync(string name);

    Task<Result<PagedResult<CityDto>>> GetByRegionAsync(
        int regionId,
        PaginationParams pagination);

    Task<Result<PagedResult<CityDto>>> Search(
       CitySearchFilter filter,
       PaginationParams pagination);
}