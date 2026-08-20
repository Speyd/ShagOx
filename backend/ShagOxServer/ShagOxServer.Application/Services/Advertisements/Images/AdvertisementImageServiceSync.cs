using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.DTOs.Advertisements.Update.Images;
using ShagOxServer.Application.DTOs.Specification.Pictures.Create;
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
        var loadedImage = new List<PictureCreateResponse>();
        var deletePublicIds = new List<string>();
        var originalImageOrders = new List<int>();

        try
        {
            if (request.Images is null ||
                !request.Images.Any())
            {
                return Result<bool>.Success(true);
            }

            originalImageOrders = 
                await PrepareOrdersAsync(advertisement);


            foreach (var image in request.Images)
            {
                await SyncImageAsync(
                    advertisement, 
                    image,
                    loadedImage,
                    deletePublicIds
                 );
            }

            await DeleteImagesByPublicIdAsync(deletePublicIds);

            return Result<bool>.Success(true);
        }
        catch
        {
            await DeleteLoadedImagesAsync(loadedImage);

            ToOriginalOrder(advertisement, originalImageOrders);

            throw;
        }
    }

    private async Task<Result<bool>> SyncImageAsync(
        Advertisement advertisement,
        ImageAdvertUpdateRequest image,
        List<PictureCreateResponse> loadedImage,
        List<string> deletePublicIds)
    {
        var getImage = advertisement.Images
                    .FirstOrDefault(x => x.Id == image.Id);

        Result<bool>? result = null;

        if (image.IsDeleted &&
            image.Id is not null)
        {
            result = await DeleteImageAsync(
               image,
               getImage,
               deletePublicIds);
        }

        if (!image.IsDeleted &&
            image.File is not null)
        {
            result = await CreateImageAsync(
                advertisement,
                image,
                loadedImage);
        }

        if (image.Id is not null)
        {
            result = UpdateImageOrder(
                image,
                getImage);
        }

        if (result is not null &&
            !result.IsSuccess)
        {
            throw new Exception();
        }

        return Result<bool>.Success(true);
    }
}