using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Location.Cities;

namespace ShagOxServer.Application.Interfaces.Location.Cities.Query;
public interface ICityQueryService
{
    Task<Result<CityDto>> GetByIdAsync(int id);

    Task<Result<CityDto>> GetByNameAsync(string name);

    Task<Result<List<CityDto>>> GetByRegionAsync(
        int regionId,
        int page = 1,
        int pageSize = 20
        );
}
