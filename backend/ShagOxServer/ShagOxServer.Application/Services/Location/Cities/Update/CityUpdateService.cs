using ShagOxServer.Application.DTOs.Location.Cities.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Update;
using ShagOxServer.Application.Services.Location.Cities.Update.Validator;
using ShagOxServer.Application.Services.Location.Cities.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.Services.Location.Cities.Update;
public class CityUpdateService : ICityUpdateService
{
    private readonly ICityRepository _cityRepository;
    private readonly CityValidator _cityValidator;
    private readonly CityUpdateValidator _cityUpdateValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CityUpdateService(
        ICityRepository cityRepository,
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

        var existsValidator = await _cityValidator.NotExistsAsync(
            changeValidator.Value!.regionId, 
            changeValidator.Value.name
        );

        if (!existsValidator.IsSuccess)
            return Result<UpdateResponse>.Fail(existsValidator.Error);


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
}