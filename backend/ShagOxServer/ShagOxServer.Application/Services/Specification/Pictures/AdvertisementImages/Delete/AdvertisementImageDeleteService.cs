using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Delete;
public class AdvertisementImageDeleteService 
    : IAdvertisementImageDeleteService
{
    private readonly IRepository<AdvertisementImage> _imageRepository;
    private readonly AdvertisementImageValidator _imageValidator;
    private readonly PictureValidator _pictureValidator;

    private readonly ImageInvalidationService _imageInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementImageDeleteService> _logger;


    public AdvertisementImageDeleteService(
        IRepository<AdvertisementImage> imageRepository,
        AdvertisementImageValidator imageValidator,
        PictureValidator pictureValidator,
        ImageInvalidationService imageInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementImageDeleteService> logger)
    {
        _imageRepository = imageRepository;
        _imageValidator = imageValidator;
        _pictureValidator = pictureValidator;
        _imageInvalid = imageInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(long id)
    {
        var image = await _imageValidator.GetByIdAsync(id);

        if (!image.IsSuccess)
            return Result<DeleteResponse>.Fail(image.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var result = await DeleteInternalAsync(image.Value!);

            if (!result.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();
                return result;
            }

            await _unitOfWork.CommitAsync();

            await _imageInvalid.InvalidateDeleteAsync(
                AdvertisementImageCacheMapper.ToInfo(image.Value!));

            return result;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete image. Id: {Id}",
                id);

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.AdvertImageDeleteFailed);
        }
    }

    public async Task<Result<DeleteResponse>> DeleteInternalAsync(
        AdvertisementImage image)
    {
        _imageRepository.Delete(image);

        var result = await _pictureValidator
            .PictureDeleteValidator(image.PublicId);

        if (!result.IsSuccess)
            return Result<DeleteResponse>.Fail(result.Error);

        _logger.LogInformation(
            "Image deleted successfully. Id: {Id}",
            image.Id);

        return Result<DeleteResponse>.Success(
            new DeleteResponse(
                image.Id,
                DateTime.UtcNow));
    }

    public async Task<Result<DeleteResponse>> DeleteRecordAsync(
        long id)
    {
        var image = await _imageValidator.GetByIdAsync(id);
        if (!image.IsSuccess)
            return Result<DeleteResponse>.Fail(image.Error);

        try
        {
            _imageRepository.Delete(image.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to delete image. Id: {Id}",
               id);

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.AdvertImageDeleteFailed);
        }

        _logger.LogInformation(
            "Image deleted successfully. Id: {Id}",
            image.Value!.Id);

        await _imageInvalid.InvalidateDeleteAsync(
            AdvertisementImageCacheMapper.ToInfo(image.Value!));

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               image.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}
