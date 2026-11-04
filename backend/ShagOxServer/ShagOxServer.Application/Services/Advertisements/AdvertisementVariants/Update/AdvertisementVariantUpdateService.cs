using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Base.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Update;
public partial class AdvertisementVariantUpdateService
    : BaseUpdateService<AdvertisementVariant>,
    IAdvertisementVariantUpdateService
{
    private readonly IRepository<AdvertisementVariant> _variantRepository;
    private readonly AdvertisementVariantValidator _variantValidator;

    private readonly AdvertisementValidator _advertValidator;

    private readonly AdvertisementVariantInvalidationService _variantInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementVariantUpdateService> _logger;


    public AdvertisementVariantUpdateService(
        IRepository<AdvertisementVariant> variantRepository,
        AdvertisementVariantValidator variantValidator,
        AdvertisementValidator advertValidator,
        AdvertisementVariantInvalidationService variantInvalid,
        IAdvertisementVariantDeleteService deleteService,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementVariantUpdateService> logger,
        IDataCacheService cache
    )
        : base(deleteService, variantValidator, 
            logger, unitOfWork)
    {
        _variantRepository = variantRepository;
        _variantValidator = variantValidator;
        _advertValidator = advertValidator;
        _variantInvalid = variantInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long valueId,
        AdvertisementVariantUpdateRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var result = await UpdateInternalAsync(valueId, request);

            if (!result.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();
                return result;
            }

            await _unitOfWork.CommitAsync();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogWarning(
                ex,
                "Concurrency conflict while updating advertisement variant." + 
                " Id: {Id}",
                valueId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.ConcurrencyConflictMessage);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update advertisement variant. Id: {Id}",
                valueId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.AdvertisementVariantUpdateFailed);
        }
    }

    public async Task<Result<UpdateResponse>> UpdateInternalAsync(
        long variantId,
        AdvertisementVariantUpdateRequest request)
    {
        var variant = await _variantValidator
            .GetByIdAsync(variantId);

        if (!variant.IsSuccess)
        {
            return Result<UpdateResponse>
                .Fail(variant.Error);
        }

        var validation = await ValidateUpdateConflictsAsync(
            variant.Value!,
            request);

        if (!validation.IsSuccess)
        {
            return Result<UpdateResponse>
                .Fail(validation.Error);
        }

        var updatedCount = AdvertisementVariantUpdater
            .ApplyUpdates(variant.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow);

        if (updatedCount == 0)
        {
            return Result<UpdateResponse>.Success(result);
        }

        _variantRepository.Update(variant.Value!);

        await _variantInvalid.InvalidateUpdateAsync(
            AdvertisementVariantCacheMapper.ToInfo(variant.Value!));

        return Result<UpdateResponse>.Success(result);
    }
}
