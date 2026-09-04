using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Location.Regions.Translations.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Update;
using ShagOxServer.Application.Services.Location.Regions.Translations.Validator;
using ShagOxServer.Application.Services.Location.Regions.Validator;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Translations.Update;
public class RegionTranslationUpdateService
    : IRegionTranslationUpdateService
{
    private readonly IRepository<RegionTranslation> _regionRepository;
    private readonly RegionTranslationValidator _regionTranslationValidator;
    private readonly RegionValidator _regionValidator;


    private readonly IUnitOfWork _unitOfWork;


    public RegionTranslationUpdateService(
        IRepository<RegionTranslation> regionRepository,
        RegionTranslationValidator regionTranslationValidator,
        RegionValidator regionValidator,
        IUnitOfWork unitOfWork)
    {
        _regionRepository = regionRepository;
        _regionTranslationValidator = regionTranslationValidator;
        _regionValidator = regionValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int statusTranslationId,
        RegionTranslationUpdateRequest request)
    {
        var status = await _regionTranslationValidator
            .GetByIdAsync(statusTranslationId);

        if (!status.IsSuccess)
            return Result<UpdateResponse>.Fail(status.Error);


        var validation = await
             ValidateUpdatesAsync(status.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = RegionTranslationUpdater
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
            _regionRepository.Update(status.Value!);

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
        RegionTranslation status,
        RegionTranslationUpdateRequest request)
    {
        if (request.RegionId is not null &&
           request.RegionId != status.RegionId)
        {
            var validator = await _regionValidator
                .ExistsByIdAsync(request.RegionId.Value);

            if (!validator.IsSuccess)
                return Result<bool>.Fail(validator.Error);
        }

        if (request.Language is not null)
        {
            if ((request.RegionId is not null &&
                request.RegionId != status.RegionId) ||
                request.Language != status.Language)
            {
                var validator = await _regionTranslationValidator
                    .ExistsAsync(
                        request.RegionId ?? status.RegionId,
                        request.Language);

                if (!validator.IsSuccess)
                    return Result<bool>.Fail(validator.Error);
            }
        }

        return Result<bool>.Success(true);
    }
}