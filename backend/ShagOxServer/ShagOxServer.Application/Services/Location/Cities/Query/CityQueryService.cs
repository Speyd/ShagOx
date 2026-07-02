using ShagOxServer.Application.DTOs.Location.Cities;
using ShagOxServer.Application.Interfaces.Location.Cities.Query;
using ShagOxServer.Application.Services.Location.Cities.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Location.Cities.Query;
public class CityQueryService : ICityQueryService
{
    private readonly ICityQueryRepository _repository;

    public CityQueryService(
        ICityQueryRepository cityRepository)
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
        PaginationParams pagination)
    {
        var cities = await _repository.GetByRegionAsync(regionId, pagination);

        return cities.ToResultList(CityMapper.ToDto);
    }

    public async Task<Result<List<CityDto>>> SearchByName(
       string name,
       PaginationParams pagination)
    {
        var cities = await _repository.SearchByName(name, pagination);

        return cities.ToResultList(CityMapper.ToDto);
    }
}
