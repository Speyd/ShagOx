using ShagOxServer.Application.DTOs.Location.Cities.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Create;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Create;
public class CityCreateService : ICityCreateService
{
    private readonly ICityRepository _cityRepository;

    private readonly IRegionRepository _regionRepository;

    private readonly IUnitOfWork _unitOfWork;


    public CityCreateService(
        ICityRepository cityRepository,
        IRegionRepository regionRepository,
        IUnitOfWork unitOfWork)
    {
        _cityRepository = cityRepository;
        _regionRepository = regionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CityCreateResponse>> CreateCityAsync(
        CityCreateRequest request)
    {
        var region = _regionRepository.GetByIdAsync(request.RegionId);
        if (region is null)
            Result<CityCreateResponse>.NotFound("Region");

        var city = CreateCity(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _cityRepository.Add(city);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

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