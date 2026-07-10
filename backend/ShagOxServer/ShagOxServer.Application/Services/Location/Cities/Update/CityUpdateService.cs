using ShagOxServer.Application.DTOs.Location.Cities.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Update;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Update;
public class CityUpdateService : ICityUpdateService
{
    private readonly ICityRepository _repository;
    private readonly ICityExistsRepository _existsrepository;
    private readonly IUnitOfWork _unitOfWork;

    public CityUpdateService(
        ICityRepository cityRepository,
        ICityExistsRepository cityExistsRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = cityRepository;
        _existsrepository = cityExistsRepository;
        _unitOfWork = unitOfWork;
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

        var exists = await _existsrepository.ExistsAsync(regionId, name);
        if (exists)
            return Result<CityUpdateResponse>.AlreadyExists("City");


        var updatedCount = ApplyUpdates(city, request);
        var result = new CityUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<CityUpdateResponse>.Success(result);


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Add(city);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }


        return Result<CityUpdateResponse>.Success(result);
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