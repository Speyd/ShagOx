using ShagOxServer.Application.DTOs.Location.Cities;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Query;
using ShagOxServer.Application.Services.Location.Cities.Mapping;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Location.Cities.Query;
public class CityQueryService 
    : ICityQueryService
{
    private readonly ICityQueryRepository _repositoryQueryCity;


    public CityQueryService(
        ICityQueryRepository repositoryQueryCity)
    {
        _repositoryQueryCity = repositoryQueryCity;
    }


    public async Task<Result<CityDto>> GetByIdAsync(int id)
    {
        var city = await _repositoryQueryCity
            .GetByIdAsync(id);

        return city.ToResult(CityMapper.ToDto);
    }

    public async Task<Result<PagedResult<CityDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var cities = await _repositoryQueryCity
            .GetPagedAsync(pagination);

        return cities.ToResultPaged(CityMapper.ToDto);
    }

    public async Task<Result<CityDto>> GetByNameAsync(string name)
    {
        var city = await _repositoryQueryCity
            .GetByNameAsync(name);

        return city.ToResult(CityMapper.ToDto);
    }

    public async Task<Result<PagedResult<CityDto>>> GetByRegionAsync(
        int regionId,
        PaginationParams pagination)
    {
        var cities = await _repositoryQueryCity
            .GetByRegionAsync(regionId, pagination);

        return cities.ToResultPaged(CityMapper.ToDto);
    }

    public async Task<Result<PagedResult<CityDto>>> Search(
       CitySearchFilter filter,
       PaginationParams pagination)
    {
        var cities = await _repositoryQueryCity
            .Search(filter, pagination);

        return cities.ToResultPaged(CityMapper.ToDto);
    }
}