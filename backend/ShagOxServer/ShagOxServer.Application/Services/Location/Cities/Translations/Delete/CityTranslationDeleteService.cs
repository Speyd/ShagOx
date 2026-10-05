using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Translations.Query.Mapping;
using ShagOxServer.Application.Services.Caches.Invalidations.Location.Translation;
using ShagOxServer.Application.Services.Location.Cities.Translations.Validator;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Translations.Delete;
public class CityTranslationDeleteService
    : ICityTranslationDeleteService
{
    private readonly IRepository<CityTranslation> _cityRepository;
    private readonly CityTranslationValidator _cityValidator;

    private readonly CityTranslationInvalidationService _cityInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CityTranslationDeleteService> _logger;


    public CityTranslationDeleteService(
        IRepository<CityTranslation> cityRepository,
        CityTranslationValidator cityValidator,
        CityTranslationInvalidationService cityInvalid,
        IUnitOfWork unitOfWork,
        ILogger<CityTranslationDeleteService> logger)
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
                 .Fail(EntityErrorResources.CityTranslationDeleteFailed);
        }

        await _cityInvalid.InvalidateDeleteAsync(
            BaseTranslationCacheMapper.ToInfo(city.Value!));

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               city.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}