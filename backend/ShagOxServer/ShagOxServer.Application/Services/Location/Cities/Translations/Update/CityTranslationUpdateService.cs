using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Location.Cities.Translations.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Update;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Location.Cities.Translations.Validator;
using ShagOxServer.Application.Services.Location.Cities.Validator;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Translations.Update;
public class CityTranslationUpdateService
    : BaseTranslationUpdateSerivce<City, CityTranslation>,
    ICityTranslationUpdateService
{
    private readonly IRepository<CityTranslation> _cityRepository;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CityTranslationUpdateService> _logger;


    public CityTranslationUpdateService(
        IRepository<CityTranslation> cityRepository,
        CityTranslationValidator cityTranslationValidator,
        CityValidator cityValidator,
        IUnitOfWork unitOfWork,
        ILogger<CityTranslationUpdateService> logger
        ) : base(cityValidator, cityTranslationValidator)
    {
        _cityRepository = cityRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int statusTranslationId,
        CityTranslationUpdateRequest request)
    {
        var city = await _translationValidator
            .GetByIdAsync(statusTranslationId);

        if (!city.IsSuccess)
            return Result<UpdateResponse>.Fail(city.Error);


        var validation = await
             ValidateUpdatesAsync(city.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = CityTranslationUpdater
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
               "Failed to update city translation. Id: {Id}",
               statusTranslationId);

            return Result<UpdateResponse>
                 .Fail("Failed to update city translation.");
        }

        return Result<UpdateResponse>.Success(result);
    }
}