using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Location.Cities.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Update;
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

    private readonly IUnitOfWork _unitOfWork;


    public CityUpdateService(
        IRepository<City> cityRepository,
        CityValidator cityValidator,
        CityUpdateValidator cityUpdateValidator,
        IUnitOfWork unitOfWork)
    {
        _cityRepository = cityRepository;
        _cityValidator = cityValidator;
        _cityUpdateValidator = cityUpdateValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int cityId, 
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


        var updatedCount = CityUpdater.ApplyUpdates(city.Value!, request);
        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _cityRepository.Add(city.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }


        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        (int regionId, string name) changeValidator,
        CityUpdateRequest request)
    {
        var existsValidator = await _cityValidator.NotExistsAsync(
            changeValidator.regionId,
            changeValidator.name
        );

        if (!existsValidator.IsSuccess)
            return Result<bool>.Fail(existsValidator.Error);

        return Result<bool>.Success(true);
    }
}