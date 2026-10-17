using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Update;
public class AdvertisementImageUpdateService 
    : PictureOrderUpdateService<Advertisement,
        AdvertisementImage,
        PictureOrderUpdateRequest>,
    IAdvertisementImageUpdateService
{
    private readonly IRepository<AdvertisementImage> _imageRepository;

    private readonly ImageInvalidationService _imageInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementImageUpdateService> _logger;


    public AdvertisementImageUpdateService(
        IRepository<AdvertisementImage> imageRepository,
        AdvertisementImageValidator imageValidator,
        AdvertisementValidator advertisementValidator,
        ImageInvalidationService imageInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementImageUpdateService> logger
    )
        : base(advertisementValidator, imageValidator)
    {
        _imageRepository = imageRepository;
        _imageInvalid = imageInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public override async Task<Result<UpdateResponse>> UpdateAsync(
        long imageId,
        PictureOrderUpdateRequest request)
    {
        var image = await _pictureValidator.GetByIdAsync(imageId);

        if (!image.IsSuccess)
        {
            return Result<UpdateResponse>.Fail(image.Error);
        }

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var result = await UpdateInternalAsync(
                image.Value!,
                request);

            if (!result.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();
                return result;
            }

            await _unitOfWork.CommitAsync();

            await _imageInvalid.InvalidateUpdateAsync(
                AdvertisementImageCacheMapper.ToInfo(image.Value!));

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogWarning(
                ex,
                "Concurrency conflict while updating image." +
                " Id: {Id}",
                imageId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.ConcurrencyConflictMessage);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update image. Id: {Id}",
                imageId);

            return Result<UpdateResponse>
                .Fail(EntityErrorResources.AdvertImageUpdateFailed);
        }
    }


    public override async Task<Result<UpdateResponse>> UpdateInternalAsync(
        AdvertisementImage image,
        PictureOrderUpdateRequest request)
    {
        var newOrder = await GetNewOrder(image.Id, request);

        if (!newOrder.IsSuccess)
        {
            return Result<UpdateResponse>.Fail(newOrder.Error);
        }

        if (newOrder.Value is null)
        {
            return Result<UpdateResponse>
                .Fail(EntityErrorResources.AdvertImageUpdateFailed);
        }

        var updatedCount = AdvertisementImageUpdater.ApplyUpdates(
            image,
            newOrder.Value.Value,
            request);

        var response = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow);

        if (updatedCount == 0)
        {
            return Result<UpdateResponse>.Success(response);
        }

        _imageRepository.Update(image);

        _logger.LogInformation(
            "Image updated successfully. Id: {Id}",
            image.Id);

        return Result<UpdateResponse>.Success(response);
    }
}
