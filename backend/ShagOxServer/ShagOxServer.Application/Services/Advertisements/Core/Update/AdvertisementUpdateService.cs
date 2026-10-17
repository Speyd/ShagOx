using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.Core.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping;
using ShagOxServer.Application.Services.Advertisements.Core.Update.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Application.Services.Specification.Pictures.Core.PictureOrdering;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Core.Update;
public partial class AdvertisementUpdateService 
    : IAdvertisementUpdateService
{
    private readonly IRepository<Advertisement> _advertRepository;

    private readonly PictureOrderSyncExecutor<Advertisement, 
        AdvertisementImage> _executor;

    private readonly AdvertisementValidator _validator;
    private readonly AdvertisementUpdateValidator _validatorUpdate;

    private readonly AdvertisementVariantValidator _variantValidator;

    private readonly IAdvertisementVariantUpdateService _variantUpdateService;

    private readonly AdvertisementInvalidationService _advertInvalid;



    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementUpdateService> _logger;


    public AdvertisementUpdateService(
        IRepository<Advertisement> advertRepository,
        PictureOrderSyncExecutor<Advertisement,
            AdvertisementImage> executor,
        AdvertisementValidator validator,
        AdvertisementUpdateValidator validatorUpdate,
        AdvertisementVariantValidator variantValidator,
        IAdvertisementVariantUpdateService variantUpdateService,
        AdvertisementInvalidationService advertInvalid,
        IFavoriteQueryRepository favoriteRepository,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementUpdateService> logger)
    {
        _validator = validator;
        _advertRepository = advertRepository;
        _validatorUpdate = validatorUpdate;
        _variantValidator = variantValidator;
        _executor = executor;
        _variantUpdateService = variantUpdateService;
        _advertInvalid = advertInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
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

        int updatedCount = 0;

        try
        {
            updatedCount = AdvertisementUpdater
                .ApplyUpdates(advert.Value!, request);

            var imagesResult = await _executor.SyncAsync(
                advert.Value!,
                request.Pictures
            );

            if (!imagesResult.IsSuccess)
            {
                return Result<UpdateResponse>
                    .Fail(imagesResult.Error!);
            }

            var updateResult = await UpdateVariantsAsync(
                variantsResult.Value);

             if (!updateResult.IsSuccess)
             {
                await _unitOfWork.RollbackAsync();

                return Result<UpdateResponse>
                    .Fail(updateResult.Error!);
            }

            _advertRepository.Update(advert.Value!);

            await _executor.CommitAndDeleteAsync(
                imagesResult.Value!
            );
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogWarning(
                ex,
                "Concurrency conflict while updating advertisement." +
                " Id: {Id}",
                advertId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.ConcurrencyConflictMessage);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update advertisement. Id: {Id}",
                advertId);

            return Result<UpdateResponse>
                .Fail(EntityErrorResources.AdvertisementUpdateFailed);
        }

        _logger.LogInformation(
                "Advertisement updated successfully. Id: {Id}",
                advert.Value!.Id);

        await _advertInvalid.InvalidateUpdateAsync(
            AdvertisementCacheMapper.ToInfo(advert.Value!));

        return Result<UpdateResponse>.Success(
            new UpdateResponse(
                updatedCount,
                DateTime.UtcNow
            ));
    }
}
