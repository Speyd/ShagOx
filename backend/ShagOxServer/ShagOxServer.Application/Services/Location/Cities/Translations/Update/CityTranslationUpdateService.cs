using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Location.Cities.Translations.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Update;
using ShagOxServer.Application.Services.Location.Cities.Translations.Validator;
using ShagOxServer.Application.Services.Location.Cities.Validator;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Translations.Update;
public class CityTranslationUpdateService
    : ICityTranslationUpdateService
{
    private readonly IRepository<CityTranslation> _cityRepository;
    private readonly CityTranslationValidator _cityTranslationValidator;
    private readonly CityValidator _cityValidator;


    private readonly IUnitOfWork _unitOfWork;


    public CityTranslationUpdateService(
        IRepository<CityTranslation> cityRepository,
        CityTranslationValidator cityTranslationValidator,
        CityValidator cityValidator,
        IUnitOfWork unitOfWork)
    {
        _cityRepository = cityRepository;
        _cityTranslationValidator = cityTranslationValidator;
        _cityValidator = cityValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int statusTranslationId,
        CityTranslationUpdateRequest request)
    {
        var status = await _cityTranslationValidator
            .GetByIdAsync(statusTranslationId);

        if (!status.IsSuccess)
            return Result<UpdateResponse>.Fail(status.Error);


        var validation = await
             ValidateUpdatesAsync(status.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = CityTranslationUpdater
            .ApplyUpdates(status.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _cityRepository.Update(status.Value!);

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
        CityTranslation status,
        CityTranslationUpdateRequest request)
    {
        if (request.CityId is not null &&
           request.CityId != status.CityId)
        {
            var validator = await _cityValidator
                .ExistsByIdAsync(request.CityId.Value);

            if (!validator.IsSuccess)
                return Result<bool>.Fail(validator.Error);
        }

        if (request.Language is not null)
        {
            if ((request.CityId is not null &&
                request.CityId != status.CityId) ||
                request.Language != status.Language)
            {
                var validator = await _cityTranslationValidator
                    .ExistsAsync(
                        request.CityId ?? status.CityId,
                        request.Language);

                if (!validator.IsSuccess)
                    return Result<bool>.Fail(validator.Error);
            }
        }

        return Result<bool>.Success(true);
    }
}