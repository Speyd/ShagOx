using ShagOxServer.Application.DTOs.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Location.Cities.Query;
public interface ICityQueryService
{
    Task<Result<CityDto>> GetByIdAsync(int id);

    Task<Result<CityDto>> GetByNameAsync(string name);

    Task<Result<List<CityDto>>> GetByRegionAsync(
        int regionId,
        PaginationParams pagination);

    Task<Result<List<CityDto>>> SearchByName(
       string name,
       PaginationParams pagination);
}
