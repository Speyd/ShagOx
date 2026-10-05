using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Location;
using ShagOxServer.Application.Services.Location.Cities.Mapping;
using ShagOxServer.Application.Services.Location.Cities.Validator;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Delete;
public class CityDeleteService 
    : ICityDeleteService
{
    private readonly IRepository<City> _cityRepository;
    private readonly CityValidator _cityValidator;

    private readonly CityInvalidationService _cityInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CityDeleteService> _logger;


    public CityDeleteService(
        IRepository<City> cityRepository,
        CityValidator cityValidator,
        CityInvalidationService cityInvalid,
        IUnitOfWork unitOfWork,
        ILogger<CityDeleteService> logger)
    {
        _cityRepository = cityRepository;
        _cityValidator = cityValidator;
        _cityInvalid = cityInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        var city = await _cityValidator.GetByIdAsync(id);
        if (!city.IsSuccess)
            return Result<DeleteResponse>.Fail(city.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _cityRepository.Delete(city.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete city. Id: {Id}",
                id);

            return Result<DeleteResponse>
                 .Fail(EntityErrorResources.CityDeleteFailed);
        }

        await _cityInvalid.InvalidateDeleteAsync(
            CityCacheMapper.ToInfo(city.Value!));

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               city.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}