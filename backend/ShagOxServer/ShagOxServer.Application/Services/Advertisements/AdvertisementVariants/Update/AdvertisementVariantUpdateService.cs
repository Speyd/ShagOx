using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Caches;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Update;
public class AdvertisementVariantUpdateService
    : IAdvertisementVariantUpdateService
{
    private readonly IRepository<AdvertisementVariant> _variantRepository;
    private readonly AdvertisementVariantValidator _variantValidator;

    private readonly AdvertisementValidator _advertValidator;


    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementVariantUpdateService> _logger;
    private readonly ICacheService _cache;


    public AdvertisementVariantUpdateService(
        IRepository<AdvertisementVariant> variantRepository,
        AdvertisementVariantValidator variantValidator,
        AdvertisementValidator advertValidator,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementVariantUpdateService> logger,
        ICacheService cache)
    {
        _variantRepository = variantRepository;
        _variantValidator = variantValidator;
        _advertValidator = advertValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _cache = cache;
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

            await AdvertisementVariantCache
                .InvalidateUpdateAsync(_cache, valueId);

            return result;
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
        var variant = await _variantValidator.GetByIdAsync(variantId);

        if (!variant.IsSuccess)
        {
            return Result<UpdateResponse>
                .Fail(variant.Error);
        }

        var validation = await ValidateUpdatesAsync(
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

        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        AdvertisementVariant variant,
        AdvertisementVariantUpdateRequest request)
    {
        var attributes = request.Attributes ?? variant.Attributes;
        var advertId = request.AdvertisementId ?? variant.AdvertisementId;

        if (advertId != variant.AdvertisementId)
        {
            var advertExists = await _advertValidator
                .ExistsByIdAsync(advertId);

            if (!advertExists.IsSuccess)
                return Result<bool>.Fail(advertExists.Error);
        }

        if (attributes != variant.Attributes ||
            advertId != variant.AdvertisementId)
        {
            var variantExists = await _variantValidator
                .NotExistsAsync(advertId, attributes);

            if (!variantExists.IsSuccess)
                return Result<bool>.Fail(variantExists.Error);
        }

        if (request.Attributes is not null)
        {
            
            var isVariantExists = await _variantValidator
                .ValidateVariantAttributesAsync(
                    advertId,
                    request.Attributes);

            if (!isVariantExists.IsSuccess)
            {
                return Result<bool>
                    .Fail(isVariantExists.Error);
            }
        }


        return Result<bool>.Success(true);
    }
}