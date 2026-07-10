using ShagOxServer.Application.DTOs.Specification.Images.Update;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Update;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Images.Update;
public class ImageUpdateService : IImageUpdateService
{
    private readonly IImageRepository _imageRepository;
    private readonly IAdvertisementRepository _advertisementRepository;


    public ImageUpdateService(
        IImageRepository imageRepository,
        IAdvertisementRepository advertisementRepository)
    {
        _imageRepository = imageRepository;
        _advertisementRepository = advertisementRepository;
    }

    public async Task<Result<ImageUpdateResponse>> UpdateImageAsync(
        int imageId,
        ImageUpdateRequest request)
    {
        var image = await _imageRepository.GetByIdAsync(imageId);

        if (image is null)
            return Result<ImageUpdateResponse>.NotFound("Image");

        int? newOrder = request.Order;

        if (request.AdvertisementId is not null && request.Order is not null)
        {
            var advert = await _advertisementRepository
                .GetByIdAsync(request.AdvertisementId.Value);

            if (advert is null)
                return Result<ImageUpdateResponse>.NotFound("Advertisement");

            var orderExists = advert.Images.Any(x =>
                x.Id != imageId &&
                x.Order == request.Order);

            if (orderExists)
                newOrder = GetNextOrder(advert);
        }

        var updatedCount = ApplyUpdates(image, newOrder, request);

        var response = new ImageUpdateResponse(
            DateTime.UtcNow,
            updatedCount);

        if (updatedCount == 0)
            return Result<ImageUpdateResponse>.Success(response);

        _imageRepository.Update(image);

        return Result<ImageUpdateResponse>.Success(response);
    }

    private static int ApplyUpdates(
        Image image,
        int? newOrder,
        ImageUpdateRequest request)
    {
        var updated = 0;

        if (request.Url is not null &&
            image.Url != request.Url)
        {
            image.Url = request.Url;
            updated++;
        }

        if (newOrder is not null &&
            image.Order != newOrder.Value)
        {
            image.Order = newOrder.Value;
            updated++;
        }

        if (request.AdvertisementId is not null &&
            image.AdvertisementId != request.AdvertisementId.Value)
        {
            image.AdvertisementId = request.AdvertisementId.Value;
            updated++;
        }

        return updated;
    }

    private static int GetNextOrder(Advertisement advert)
    {
        return advert.Images
            .Select(x => x.Order)
            .DefaultIfEmpty(0)
            .Max() + 1;
    }
}
