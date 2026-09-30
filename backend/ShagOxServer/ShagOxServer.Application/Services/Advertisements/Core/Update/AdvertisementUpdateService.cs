using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.DTOs.Advertisements.Core.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Images;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Resources.Validations;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Update.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Caches.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Advertisements.Core.Update;
public class AdvertisementUpdateService 
    : IAdvertisementUpdateService
{
    private readonly IRepository<Advertisement> _advertRepository;
    private readonly IAdvertisementImageService _imageService;
    private readonly AdvertisementValidator _validator;
    private readonly AdvertisementUpdateValidator _validatorUpdate;

    private readonly AdvertisementVariantValidator _variantValidator;

    private readonly IAdvertisementVariantUpdateService _variantUpdateService;
    private readonly IFavoriteQueryRepository _favoriteRepository;


    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementUpdateService> _logger;
    private readonly ICacheService _cache;


    public AdvertisementUpdateService(
        IRepository<Advertisement> advertRepository,
        IAdvertisementImageService imageService,
        AdvertisementValidator validator,
        AdvertisementUpdateValidator validatorUpdate,
        AdvertisementVariantValidator variantValidator,
        IAdvertisementVariantUpdateService variantUpdateService,
        IFavoriteQueryRepository favoriteRepository,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementUpdateService> logger,
        ICacheService cache)
    {
        _validator = validator;
        _advertRepository = advertRepository;
        _validatorUpdate = validatorUpdate;
        _variantValidator = variantValidator;
        _imageService = imageService;
        _variantUpdateService = variantUpdateService;
        _favoriteRepository = favoriteRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _cache = cache;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long advertId,
        AdvertisementUpdateRequest request)
    {
        var advert = await _validator
            .GetByIdWithIncludeAsync(advertId);
        if (!advert.IsSuccess)
            return Result<UpdateResponse>.Fail(advert.Error);

        var validation = await _validatorUpdate
            .ValidateAsync(request);
        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error!);

        var variantsResult = ParseAndValidateVariants(request.Variants);

        if (!variantsResult.IsSuccess)
        {
            return Result<UpdateResponse>
                .Fail(variantsResult.Error!);
        }

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var updatedCount = AdvertisementUpdater
                .ApplyUpdates(advert.Value!, request);

            var imagesResult = await _imageService
                .SyncImagesAsync(
                    advert.Value!,
                    request
                );

            if (!imagesResult.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<UpdateResponse>
                    .Fail(imagesResult.Error!);
            }

            if (variantsResult.Value is not null)
            {
                var updateResult = await UpdateVariantsAsync(variantsResult.Value);

                if (!updateResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();

                    return Result<UpdateResponse>
                        .Fail(updateResult.Error!);
                }
            }

            _advertRepository.Update(advert.Value!);

            await _unitOfWork.CommitAsync();

            await AdvertisementCache.InvalidateUpdateAsync(
                 _cache,
                 _favoriteRepository,
                 advert.Value!);

            _logger.LogInformation(
                "Advertisement updated successfully. Id: {Id}",
                advert.Value!.Id);

            return Result<UpdateResponse>.Success(
                new UpdateResponse(
                    updatedCount,
                    DateTime.UtcNow
                ));
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update advertisement. Id: {Id}",
                advertId);

            return Result<UpdateResponse>
                    .Fail(EntityErrorResources.AdvertisementUpdateFailed);
        }
    }

    private Result<Dictionary<long, AdvertisementVariantUpdateRequest>?>
        ParseAndValidateVariants(string? variantsJson)
    {
        if (string.IsNullOrWhiteSpace(variantsJson))
        {
            return Result<Dictionary<long,
                    AdvertisementVariantUpdateRequest>?>
                .Success(null);
        }

        try
        {
            var variants = JsonSerializer.Deserialize<
                Dictionary<long, AdvertisementVariantUpdateRequest>
                    >(variantsJson);

            if (variants is null)
            {
                return Result<Dictionary<long,
                        AdvertisementVariantUpdateRequest>?>
                    .Success(null);
            }

            var duplicateExists = _variantValidator
                .ValidateUniqueAttributes(
                    variants.Values
                        .Select(x => x.Attributes)
                        .ToList()
                );

            if (!duplicateExists.IsSuccess)
            {
                return Result<Dictionary<long, 
                        AdvertisementVariantUpdateRequest>?>
                    .Fail(duplicateExists.Error!);
            }

            return Result<Dictionary<long,
                    AdvertisementVariantUpdateRequest>?>
                .Success(variants);
        }
        catch (JsonException)
        {
            return Result<Dictionary<long, AdvertisementVariantUpdateRequest>?>
                .Fail(ValidationResources.InvalidJson);
        }
    }

    private async Task<Result<bool>> UpdateVariantsAsync(
        Dictionary<long, AdvertisementVariantUpdateRequest> variants)
    {
        foreach (var variant in variants)
        {
            var updateResult = await _variantUpdateService
                .UpdateInternalAsync(
                    variant.Key,
                    variant.Value
                );

            if (!updateResult.IsSuccess)
            {
                _logger.LogError(
                    "Failed to update advertisement variant(variant update). " +
                    "VariantId: {VariantId}, Error: {Error}",
                    variant.Key,
                    updateResult.Error
                );

                return Result<bool>.Fail(updateResult.Error!);
            }
        }

        return Result<bool>.Success(true);
    }
}