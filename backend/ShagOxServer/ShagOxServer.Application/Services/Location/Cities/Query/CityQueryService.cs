using ShagOxServer.Application.DTOs.Location.Cities;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Location.Cities.Mapping;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Location.Cities.Query;
public class CityQueryService 
    : BaseQueryService<
        CityDto,
        City,
        CitySearchFilter
        >,
    ICityQueryService
{
    private readonly ICityQueryRepository _repositoryQueryCity;


    public CityQueryService(
        ICityQueryRepository repositoryQueryCity
    )
        : base(repositoryQueryCity)
    {
        _repositoryQueryCity = repositoryQueryCity;
    }


    protected override CityDto ApplyMapper(
        City entity)
    {
        return CityMapper.ToDto(entity);
    }

    public async Task<Result<CityDto>> GetByCodeAsync(
        string code)
    {
        var city = await _repositoryQueryCity
            .GetByCodeAsync(code);

        return city.ToResult(CityMapper.ToDto);
    }

    public async Task<Result<PagedResult<CityDto>>> GetByRegionAsync(
        long regionId,
        PaginationParams pagination)
    {
        var cities = await _repositoryQueryCity
            .GetByRegionAsync(regionId, pagination);

        return cities.ToResultPaged(CityMapper.ToDto);
    }
}