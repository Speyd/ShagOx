using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Location.Regions.Translations.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Update;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Location.Regions.Translations.Validator;
using ShagOxServer.Application.Services.Location.Regions.Validator;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Translations.Update;
public class RegionTranslationUpdateService
    : BaseTranslationUpdateSerivce<Region, RegionTranslation>,
    IRegionTranslationUpdateService
{
    private readonly IRepository<RegionTranslation> _regionRepository;
    private readonly RegionTranslationValidator _regionTranslationValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegionTranslationUpdateService> _logger;


    public RegionTranslationUpdateService(
        IRepository<RegionTranslation> regionRepository,
        RegionTranslationValidator regionTranslationValidator,
        RegionValidator regionValidator,
        IUnitOfWork unitOfWork,
        ILogger<RegionTranslationUpdateService> logger
    ) : base(regionValidator, regionTranslationValidator)
    {
        _regionRepository = regionRepository;
        _regionTranslationValidator = regionTranslationValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int statusTranslationId,
        RegionTranslationUpdateRequest request)
    {
        var region = await _regionTranslationValidator
            .GetByIdAsync(statusTranslationId);

        if (!region.IsSuccess)
            return Result<UpdateResponse>.Fail(region.Error);


        var validation = await
             ValidateUpdatesAsync(region.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = RegionTranslationUpdater
            .ApplyUpdates(region.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _regionRepository.Update(region.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update region translation. Id: {Id}",
                statusTranslationId);

            return Result<UpdateResponse>
                   .Fail("Failed to update region translation.");
        }

        return Result<UpdateResponse>.Success(result);
    }
}