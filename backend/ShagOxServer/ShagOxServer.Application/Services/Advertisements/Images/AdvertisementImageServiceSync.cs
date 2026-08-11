using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.DTOs.Advertisements.Update.Images;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Images;

public partial class AdvertisementImageService
{
    public async Task<Result<bool>> SyncImagesAsync(
        int advertisementId,
        AdvertisementUpdateRequest request)
    {
        var advertisement = await _advertValidator
            .GetByIdAsync(advertisementId);

        if (!advertisement.IsSuccess)
            return Result<bool>.Fail(advertisement.Error);

        return await SyncImagesAsync(
            advertisement.Value!,
            request);
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
}