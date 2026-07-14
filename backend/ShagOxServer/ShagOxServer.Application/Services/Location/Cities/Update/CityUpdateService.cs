using ShagOxServer.Application.DTOs.Location.Cities.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Update;
using ShagOxServer.Application.Services.Location.Cities.Update.Validator;
using ShagOxServer.Application.Services.Location.Cities.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Update;
public class CityUpdateService : ICityUpdateService
{
    private readonly ICityRepository _repository;
    private readonly CityValidator _validator;
    private readonly CityUpdateValidator _updateValidator;

    private readonly IUnitOfWork _unitOfWork;

    public CityUpdateService(
        ICityRepository cityRepository,
        CityValidator validator,
        CityUpdateValidator updateValidator,
        IUnitOfWork unitOfWork)
    {
        _repository = cityRepository;
        _validator = validator;
        _updateValidator = updateValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CityUpdateResponse>> UpdateCityAsync(
        int cityId, 
        CityUpdateRequest request)
    {
        var city = await _validator.GetByIdAsync(cityId);
        if (!city.IsSuccess)
            return Result<CityUpdateResponse>.Fail(city.Error ?? "");

        var changeValidator = _updateValidator
            .HasChangesValidator(city.Value!, request);

        if (!changeValidator.IsSuccess)
        {
            return Result<CityUpdateResponse>.Success(
                new CityUpdateResponse(
                DateTime.UtcNow,
                0
            ));
        }

        var existsValidator = await _validator.NotExistsAsync(
            changeValidator.Value!.regionId, 
            changeValidator.Value.name
        );

        if (!existsValidator.IsSuccess)
            return Result<CityUpdateResponse>.Fail(existsValidator.Error ?? "");


        var updatedCount = CityUpdater.ApplyUpdates(city.Value!, request);
        var result = new CityUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<CityUpdateResponse>.Success(result);


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Add(city.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }


        return Result<CityUpdateResponse>.Success(result);
    }
}