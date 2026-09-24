using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Location.Cities.Validator;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;
using Twilio.Http;

namespace ShagOxServer.Application.Services.Location.Cities.Delete;
public class CityDeleteService 
    : ICityDeleteService
{
    private readonly IRepository<City> _cityRepository;
    private readonly CityValidator _cityValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CityDeleteService> _logger;


    public CityDeleteService(
        IRepository<City> cityRepository,
        CityValidator cityValidator,
        IUnitOfWork unitOfWork,
        ILogger<CityDeleteService> logger)
    {
        _cityRepository = cityRepository;
        _cityValidator = cityValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
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
                 .Fail(EntityError.CityDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               city.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}