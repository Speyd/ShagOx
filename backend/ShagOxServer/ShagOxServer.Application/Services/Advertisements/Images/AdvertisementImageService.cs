using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.DTOs.Advertisements.Update.Images;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.DTOs.Specification.Images.Create.File;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Images;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Images.Delete;
using ShagOxServer.Application.Services.Advertisements.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Images;
public class AdvertisementImageService : IAdvertisementImageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageQueryRepository _imageQueryRepository;
    private readonly IImageCreateService _imageCreateService;
    private readonly IImageDeleteService _imageDeleteService;
    private readonly IImageLoaderService _imageLoaderService;
    private readonly AdvertisementValidator _advertValidator;

    public AdvertisementImageService(
        IUnitOfWork unitOfWork,
        IImageQueryRepository imageQueryRepository,
        IImageCreateService imageCreateService,
        IImageDeleteService imageDeleteService,
        IImageLoaderService imageLoaderService,
        AdvertisementValidator advertValidator)
    {
        _unitOfWork = unitOfWork;
        _imageQueryRepository = imageQueryRepository;
        _imageCreateService = imageCreateService;
        _imageDeleteService = imageDeleteService;
        _imageLoaderService = imageLoaderService;
        _advertValidator = advertValidator;
    }

    public async Task<Result<bool>> SyncImagesAsync(
       int advertisementId,
       AdvertisementUpdateRequest request)
    {
        var advertisement = await _advertValidator
            .GetByIdAsync(advertisementId);

        if (!advertisement.IsSuccess)
            return Result<bool>.Fail(advertisement.Error);

        return await SyncImagesAsync(advertisement.Value!, request);
    }

    public async Task<Result<bool>> SyncImagesAsync(
        Advertisement advertisement,
        AdvertisementUpdateRequest request)
    {
        var loadedImage = new List<ImageCreateResponse>();
        var deleteImage = new List<string>();


        try
        {
            if (request.Images is null ||
                !request.Images.Any())
            {
                return Result<bool>.Success(true);
            }

            var prepareResult = await PrepareOrdersAsync(
                advertisement,
                request.Images);

            if (!prepareResult.IsSuccess)
                return prepareResult;


            foreach (var image in request.Images)
            {
                var result = await SyncImageAsync(
                    advertisement,
                    image,
                    loadedImage,
                    deleteImage);

                if (!result.IsSuccess)
                    return result;
            }

            foreach (var publicId in deleteImage)
            {
                await _imageLoaderService.DeleteAsync(publicId);
            }

            return Result<bool>.Success(true);
        }
        catch
        {
            await DeleteLoadedImagesAsync(loadedImage);
            throw;
        }
    }

    private async Task<Result<bool>> PrepareOrdersAsync(
        Advertisement advertisement,
        List<ImageAdvertUpdateRequest> images)
    {
        var ids = images
            .Where(image => image.Id is not null)
            .Select(image => image.Id!.Value)
            .ToHashSet();

        var existingImages = advertisement.Images
            .Where(image => ids.Contains(image.Id))
            .ToList();

        if (!existingImages.Any())
            return Result<bool>.Success(true);

        foreach (var image in existingImages)
        {
            image.Order = -image.Id;
        }

        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> SyncImageAsync(
        Advertisement advertisement,
        ImageAdvertUpdateRequest image,
        List<ImageCreateResponse> loadedImage,
        List<string> deleteImage)
    {
        var getImage = image.Id is null
            ? null
            : await _imageQueryRepository
                .GetByIdAsync(image.Id.Value);


        if (image.IsDeleted &&
            image.Id is not null)
        {
            return await DeleteImageAsync(
                image,
                getImage,
                deleteImage);
        }


        if (!image.IsDeleted &&
            image.File is not null)
        {
            return await CreateImageAsync(
                advertisement.Id,
                image,
                loadedImage);
        }


        if (image.Id is not null)
        {
            return UpdateImageOrder(
                image,
                getImage);
        }


        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> DeleteImageAsync(
        ImageAdvertUpdateRequest image,
        Image? getImage,
        List<string> deleteImage)
    {
        if (getImage is null)
        {
            return Result<bool>
                .NotFound($"Image({image.Id})");
        }


        var result = await _imageDeleteService
            .DeleteRecordAsync(image.Id!.Value);


        if (!result.IsSuccess)
        {
            return Result<bool>
                .Fail(result.Error!);
        }

        deleteImage.Add(getImage.PublicId);

        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> CreateImageAsync(
        int advertisementId,
        ImageAdvertUpdateRequest image,
        List<ImageCreateResponse> loadedImage)
    {
        var result = await _imageCreateService
            .CreateFromFileAsync(
                new ImageFileCreateRequest(
                    image.File!,
                    advertisementId,
                    image.Order));


        if (!result.IsSuccess)
        {
            return Result<bool>
                .Fail(result.Error!);
        }


        if (result.Value is not null)
        {
            loadedImage.Add(result.Value);
        }


        return Result<bool>.Success(true);
    }

    private Result<bool> UpdateImageOrder(
        ImageAdvertUpdateRequest image,
        Image? getImage)
    {
        if (getImage is null)
        {
            return Result<bool>
                .NotFound($"Image({image.Id})");
        }


        getImage.Order = image.Order;


        return Result<bool>.Success(true);
    }

    private async Task DeleteLoadedImagesAsync(
        List<ImageCreateResponse> loadedImage)
    {
        foreach (var image in loadedImage)
        {
            await _imageLoaderService
                .DeleteAsync(image.PublicId);
        }
    }
}