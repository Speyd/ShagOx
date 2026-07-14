using ShagOxServer.Application.DTOs.Location.Cities.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Create;
using ShagOxServer.Application.Services.Location.Cities.Validator;
using ShagOxServer.Application.Services.Location.Regions.Validator;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Create;
public class CityCreateService : ICityCreateService
{
    private readonly ICityRepository _cityRepository;
    private readonly CityValidator _cityValidator;

    private readonly IRegionRepository _regionRepository;
    private readonly RegionValidator _regionValidator;


    private readonly IUnitOfWork _unitOfWork;


    public CityCreateService(
        ICityRepository cityRepository,
        CityValidator cityValidator,
        IRegionRepository regionRepository,
        RegionValidator regionValidator,
        IUnitOfWork unitOfWork)
    {
        _cityRepository = cityRepository;
        _cityValidator = cityValidator;
        _regionRepository = regionRepository;
        _regionValidator = regionValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CityCreateResponse>> CreateCityAsync(
        CityCreateRequest request)
    {
        var region = await _regionValidator.ExistsByIdAsync(request.RegionId);
        if (!region.IsSuccess)
            Result<CityCreateResponse>.Fail(region.Error ?? "");

        var validatorName = await _cityValidator.NotExistsAsync(request.RegionId, request.Name);
        if (!validatorName.IsSuccess)
            Result<CityCreateResponse>.Fail(validatorName.Error ?? "");

        var city = CityCreater.CreateCity(request);

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
}