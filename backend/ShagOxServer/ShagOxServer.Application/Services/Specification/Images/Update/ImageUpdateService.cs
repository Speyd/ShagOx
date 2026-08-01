using ShagOxServer.Application.DTOs.Specification.Images.Update;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Application.Interfaces.Services.Specification.Images.Update;
using ShagOxServer.Application.Services.Advertisements.Validator;
using ShagOxServer.Application.Services.Specification.Images.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.Services.Specification.Images.Update;
public class ImageUpdateService : IImageUpdateService
{
    private readonly IImageRepository _imageRepository;
    private readonly ImageValidator _imageValidator;

    private readonly AdvertisementValidator _advertValidator;


    public ImageUpdateService(
        IImageRepository imageRepository,
        ImageValidator imageValidator,
        AdvertisementValidator advertisementValidator)
    {
        _imageRepository = imageRepository;
        _imageValidator = imageValidator;
        _advertValidator = advertisementValidator;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int imageId,
        ImageUpdateRequest request)
    {
        var image = await _imageValidator.GetByIdAsync(imageId);
        if (!image.IsSuccess)
            return Result<UpdateResponse>.Fail(image.Error);

        int? newOrder = request.Order;

        if (request.AdvertisementId is not null &&
            request.Order is not null)
        {
            var advert = await _advertValidator.GetByIdAsync(request.AdvertisementId.Value);

            var orderExists = advert.Value!.Images.Any(x =>
                x.Id != imageId &&
                x.Order == request.Order);

            if (orderExists)
                newOrder = ImageUpdater.GetNextOrder(advert.Value);
        }

        var updatedCount = ImageUpdater.ApplyUpdates(image.Value!, newOrder, request);

        var response = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(response);

        _imageRepository.Update(image.Value!);

        return Result<UpdateResponse>.Success(response);
    }
}