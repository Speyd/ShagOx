using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Location.Cities;
using ShagOxServer.Application.Interfaces.Location.Cities.Query;
using ShagOxServer.Application.Services.Location.Cities.Mapping;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Infrastructure.Interfaces.Location.Cities;

namespace ShagOxServer.Application.Services.Location.Cities.Query;
public class CityQueryService : ICityQueryService
{
    private readonly ICityRepository _repository;

    public CityQueryService(
        ICityRepository cityRepository)
    {
        _repository = cityRepository;
    }

    public async Task<Result<CityDto>> GetByIdAsync(int id)
    {
        var city = await _repository.GetByIdAsync(id);

        return city.ToResult(CityMapper.ToDto);
    }

    public async Task<Result<CityDto>> GetByNameAsync(string name)
    {
        var city = await _repository.GetByNameAsync(name);

        return city.ToResult(CityMapper.ToDto);
    }

    public async Task<Result<List<CityDto>>> GetByRegionAsync(
        int regionId,
        int page = 1,
        int pageSize = 20)
    {
        var cities = await _repository.GetByRegionAsync(regionId) ?? new List<City>();

        return cities.ToResultList(CityMapper.ToDto);
    }
}
