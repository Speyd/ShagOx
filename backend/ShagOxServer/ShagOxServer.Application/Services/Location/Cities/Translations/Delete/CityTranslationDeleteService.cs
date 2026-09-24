using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Location.Cities.Translations.Validator;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Translations.Delete;
public class CityTranslationDeleteService
    : ICityTranslationDeleteService
{
    private readonly IRepository<CityTranslation> _cityRepository;
    private readonly CityTranslationValidator _cityValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CityTranslationDeleteService> _logger;


    public CityTranslationDeleteService(
        IRepository<CityTranslation> cityRepository,
        CityTranslationValidator cityValidator,
        IUnitOfWork unitOfWork,
        ILogger<CityTranslationDeleteService> logger)
    {
        _cityRepository = cityRepository;
        _cityValidator = cityValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var city = await _cityValidator
            .GetByIdAsync(id);

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
               "Failed to delete city translation. Id: {Id}",
               id);

            return Result<DeleteResponse>
                 .Fail(EntityError.CityTranslationDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               city.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}