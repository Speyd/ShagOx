using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.DTOs.Advertisements.Update.Images;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.DTOs.Specification.Images.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Images;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Delete;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Images;
public class AdvertisementImageService : IAdvertisementImageService
{
    private readonly IImageQueryRepository _imageQueryRepository;
    private readonly IImageCreateService _imageCreateService;
    private readonly IImageDeleteService _imageDeleteService;
    private readonly IImageLoaderService _imageLoaderService;
    private readonly IAdvertisementQueryRepository _advertRepository;

    public AdvertisementImageService(
        IImageQueryRepository imageQueryRepository,
        IImageCreateService imageCreateService,
        IImageDeleteService imageDeleteService,
        IImageLoaderService imageLoaderService,
        IAdvertisementQueryRepository advertRepository)
    {
        _imageQueryRepository = imageQueryRepository;
        _imageCreateService = imageCreateService;
        _imageDeleteService = imageDeleteService;
        _imageLoaderService = imageLoaderService;
        _advertRepository = advertRepository;
    }

    public async Task<Result<bool>> SyncImagesAsync(
       int advertisementId,
       AdvertisementUpdateRequest request)
    {
        var advertisement = await _advertRepository
            .GetByIdAsync(advertisementId);

        if (advertisement is null)
            return Result<bool>.NotFound("Advertisement");

        return await SyncImagesAsync(advertisement, request);
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
            .DeleteImageRecordAsync(image.Id!.Value);


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