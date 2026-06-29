using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Location.Cities.Update;
using ShagOxServer.Application.Interfaces.Location.Cities.Update;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Infrastructure.Interfaces.Location.Cities;

namespace ShagOxServer.Application.Services.Location.Cities.Update;
public class CityUpdateService : ICityUpdateService
{
    private readonly ICityRepository _repository;

    public CityUpdateService(
        ICityRepository cityRepository)
    {
        _repository = cityRepository;
    }

    public async Task<Result<CityUpdateResponse>> UpdateCityAsync(
        int cityId, 
        CityUpdateRequest request)
    {
        var city = await _repository.GetByIdAsync(cityId);

        if (city is null)
            return Result<CityUpdateResponse>.NotFound("City");

        var regionId = request.RegionId ?? city.RegionId;
        var name = request.Name ?? city.Name;

        if (regionId == city.RegionId && name == city.Name)
            return Result<CityUpdateResponse>.Fail("Nothing to update");

        var exists = await _repository.ExistsAsync(regionId, name);

        if (exists)
            return Result<CityUpdateResponse>.AlreadyExists("City");

        var updatedCount = ApplyUpdates(city, request);

        if (updatedCount == 0)
            return Result<CityUpdateResponse>.Fail(
                "No fields to update");

        await _repository.UpdateAsync(city);

        return Result<CityUpdateResponse>.Success(
            new CityUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            )
        );
    }

    private static int ApplyUpdates(
        City city,
        CityUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Name is not null)
        {
            city.Name = request.Name;
            countUpdated++;
        }

        if (request.RegionId is not null)
        {
            city.RegionId = request.RegionId.Value;
            countUpdated++;
        }

        return countUpdated;
    }
}
