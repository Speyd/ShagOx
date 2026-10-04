using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Location.Cities.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Location;
using ShagOxServer.Application.Services.Location.Cities.Mapping;
using ShagOxServer.Application.Services.Location.Cities.Update.Validator;
using ShagOxServer.Application.Services.Location.Cities.Validator;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Update;
public class CityUpdateService 
    : ICityUpdateService
{
    private readonly IRepository<City> _cityRepository;
    private readonly CityValidator _cityValidator;
    private readonly CityUpdateValidator _cityUpdateValidator;

    private readonly CityInvalidationService _cityInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CityUpdateService> _logger;


    public CityUpdateService(
        IRepository<City> cityRepository,
        CityValidator cityValidator,
        CityUpdateValidator cityUpdateValidator,
        CityInvalidationService cityInvalid,
        IUnitOfWork unitOfWork,
        ILogger<CityUpdateService> logger)
    {
        _cityRepository = cityRepository;
        _cityValidator = cityValidator;
        _cityUpdateValidator = cityUpdateValidator;
        _cityInvalid = cityInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long cityId, 
        CityUpdateRequest request)
    {
        var city = await _cityValidator.GetByIdAsync(cityId);
        if (!city.IsSuccess)
            return Result<UpdateResponse>.Fail(city.Error);

        var changeValidator = _cityUpdateValidator
            .HasChangesValidator(city.Value!, request);

        if (!changeValidator.IsSuccess)
        {
            return Result<UpdateResponse>.Success(
                new UpdateResponse(
                    0,
                    DateTime.UtcNow
            ));
        }

        var validation = await
             ValidateUpdatesAsync(changeValidator.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = CityUpdater
            .ApplyUpdates(city.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _cityRepository.Update(city.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to update city. Id: {Id}",
               cityId);

            return Result<UpdateResponse>
                 .Fail(EntityErrorResources.CityUpdateFailed);
        }

        await _cityInvalid.InvalidateUpdateAsync(
            CityCacheMapper.ToInfo(city.Value!));

        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        (long regionId, string name) changeValidator,
        CityUpdateRequest request)
    {
        var existsValidator = await _cityValidator.NotExistsAsync(
            changeValidator.regionId,
            changeValidator.name
        );

        return existsValidator;
    }
}