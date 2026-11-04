using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Base.Translations.Query.Mapping;
using ShagOxServer.Application.Services.Caches.Invalidations.Location.Translation;
using ShagOxServer.Application.Services.Location.Regions.Translations.Validator;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Translations.Delete;
public class RegionTranslationDeleteService
    : BaseDeleteService<RegionTranslation>,
    IRegionTranslationDeleteService
{
    private readonly IRepository<RegionTranslation> _regionRepository;
    private readonly RegionTranslationValidator _regionValidator;

    private readonly RegionTranslationInvalidationService _trnsInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegionTranslationDeleteService> _logger;


    public RegionTranslationDeleteService(
        IRepository<RegionTranslation> regionRepository,
        RegionTranslationValidator regionValidator,
        RegionTranslationInvalidationService trnsInvalid,
        IUnitOfWork unitOfWork,
        ILogger<RegionTranslationDeleteService> logger
    )
        : base(regionValidator, unitOfWork, logger)
    {
        _regionRepository = regionRepository;
        _regionValidator = regionValidator;
        _trnsInvalid = trnsInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        var region = await _regionValidator
            .GetByIdAsync(id);

        if (!region.IsSuccess)
            return Result<DeleteResponse>.Fail(region.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _regionRepository.Delete(region.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete region translation. Id: {Id}",
                id);

            return Result<DeleteResponse>
                  .Fail(EntityErrorResources.RegionTranslationDeleteFailed);
        }

        await InvalidateCache(region.Value!);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               region.Value!.Id,
               DateTime.UtcNow
           )
       );
    }

    public override async Task InvalidateCache(
        RegionTranslation entity)
    {
        await _trnsInvalid.InvalidateCreateAsync(
            BaseTranslationCacheMapper.ToInfo(entity));
    }
}
