using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Location.Cities.Create;
using ShagOxServer.Application.Interfaces.Location.Cities.Create;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Infrastructure.Interfaces.Location.Cities;
using ShagOxServer.Infrastructure.Interfaces.Location.Regions;

namespace ShagOxServer.Application.Services.Location.Cities.Create;
public class CityCreateService : ICityCreateService
{
    private readonly ICityRepository _cityRepository;
    private readonly IRegionRepository _regionRepository;


    public CityCreateService(
        ICityRepository cityRepository,
        IRegionRepository regionRepository)
    {
        _cityRepository = cityRepository;
        _regionRepository = regionRepository;
    }

    public async Task<Result<CityCreateResponse>> CreateCityAsync(
        CityCreateRequest request)
    {
        var region = _regionRepository.GetByIdAsync(request.RegionId);
        if (region is null)
            Result<CityCreateResponse>.NotFound("Region");

        var city = CreateCity(request);

        await _cityRepository.AddAsync(city);

        var response = new CityCreateResponse(
            city.Id,
            DateTime.UtcNow
        );

        return Result<CityCreateResponse>.Success(response);
    }

    private City CreateCity(
        CityCreateRequest request)
    {
        return new City
        {
            Name = request.Name,
            RegionId = request.RegionId,
        };
    }
}
